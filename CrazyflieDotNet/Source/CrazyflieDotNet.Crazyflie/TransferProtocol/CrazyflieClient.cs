using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using CrazyflieDotNet.Crazyradio.Driver;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class CrazyflieClient : IDisposable
    {
        private readonly ICrazyradioDriver _driver;
        private CancellationTokenSource _cts;
        private Task _task;
        private readonly Subject<float> _linkQuality;
        private readonly ConcurrentQueue<Exchange> _exchangeQueue;
        private readonly ConcurrentDictionary<TryParse, byte> _listeners;

        public CrazyflieClient(ICrazyradioDriver driver)
        {
            _driver = driver;
            _linkQuality = new Subject<float>();
            _exchangeQueue = new ConcurrentQueue<Exchange>();
            _listeners = new ConcurrentDictionary<TryParse, byte>();
        }

        public void Connect()
        {
            _cts = new CancellationTokenSource();
            _task = Task.Factory.StartNew(QueueProcessor, _cts.Token);
        }

        public void Close()
        {
            _cts.Cancel();
            _task.Wait();
            _linkQuality.Dispose();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        ~CrazyflieClient()
        {
            Close();
        }

        public void Send(ICommand command)
        {
            var requestData = command.GetCommandBytes();
            _exchangeQueue.Enqueue(new Exchange(requestData, _ => true));
        }

        public Task<TResult> SendAsync<TResult>(ICommandExchange<TResult> exchange)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            TryParse handler = responseData =>
            {
                TResult result;
                if (exchange.TryParse(responseData, out result))
                {
                    completionSource.SetResult(result);
                    return true;
                }
                return false;
            };
            var requestData = exchange.GetCommandBytes();
            _exchangeQueue.Enqueue(new Exchange(requestData, handler));
            var task = completionSource.Task;
            return task;
        }

        public IObservable<TResult> Listen<TResult>(IResponceParser<TResult> parser)
        {
            Func<IObserver<TResult>, IDisposable> subscribe = observer =>
            {
                var complete = false;

                TryParse handler = responseData =>
                {
                    if (complete || _cts.IsCancellationRequested)
                    {
                        observer.OnCompleted();
                        return true;
                    }
                    TResult result;
                    if (parser.TryParse(responseData, out result))
                    {
                        observer.OnNext(result);
                    }
                    return false;
                };

                _listeners.TryAdd(handler, default(byte));

                return Disposable.Create(() =>
                                         {
                                             complete = true;
                                             byte dontCare;
                                             _listeners.TryRemove(handler, out dontCare);
                                         });
            };
            return Observable.Create(subscribe);
        }

        public TResult Send<TResult>(ICommandExchange<TResult> exchange)
        {
            var task = SendAsync(exchange);
            return task.Result;
        }

        private void QueueProcessor()
        {
            var pingTask = new Exchange(new byte[] {0xff}, _ => true);

            while (!_cts.Token.IsCancellationRequested)
            {
                Exchange exchange;
                exchange = _exchangeQueue.TryDequeue(out exchange) ? exchange : pingTask;

                while (true)
                {
                    var rawResponseData = _driver.SendData(exchange.RequestData);
                    if (!Ack.IsRecieved(rawResponseData))
                    {
                        //
                        continue;
                    }

                    _linkQuality.OnNext(1);

                    if (rawResponseData.Length == 1)
                    {
                        continue;
                    }

                    var responseData = rawResponseData.Skip(1).ToArray();

                    var listeners = _listeners.Keys;
                    foreach (var listener in listeners)
                    {
                        listener(responseData);
                    }

                    if (exchange.TryParse(responseData))
                    {
                        break;
                    }
                }
            }
        }

        private delegate bool TryParse(byte[] responseData);

        private struct Exchange
        {
            public readonly byte[] RequestData;
            public readonly TryParse TryParse;

            public Exchange(byte[] requestData, TryParse tryParse)
            {
                RequestData = requestData;
                TryParse = tryParse;
            }
        }
    }
}