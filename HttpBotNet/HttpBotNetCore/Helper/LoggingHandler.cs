using BotNetCore.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BotNetCore.Helper
{
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        private event EventHandler<GenericEventArgs<HttpRequestMessage>> _onRequestAvailable;
        private event EventHandler<GenericEventArgs<string>> _onRequestContentAvailable;
        private event EventHandler<GenericEventArgs<string,byte[]>> _onResponseContentAvailable;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Request:");
                Console.WriteLine($"RequestUri:{request.RequestUri}");
                Console.WriteLine(request.ToString());

                if (request != null)
                    if (_onRequestAvailable != null)
                        _onRequestAvailable(this, new GenericEventArgs<HttpRequestMessage>(request));
                if (request.Content != null)
                {
                    if (_onRequestContentAvailable != null)
                        _onRequestContentAvailable(this, new GenericEventArgs<string>(await request.Content.ReadAsStringAsync()));

                }
                Console.WriteLine();

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                Console.WriteLine(response.ToString());
                response.EnsureSuccessStatusCode();
                if (response.Content != null)
                {
                    if (_onResponseContentAvailable != null)
                        _onResponseContentAvailable(this, new GenericEventArgs<string, byte[]>(request.GetHashCode().ToString(), await response.Content.ReadAsByteArrayAsync()));
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                Console.WriteLine($"Response: Success? => {response.IsSuccessStatusCode}");
                Console.WriteLine($"Response: HttpStatuscode? => {response.StatusCode}");
                Console.WriteLine($"RequestMessageToResponse? => {response.RequestMessage}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                Console.WriteLine();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()) ;
                return null;
            }
            
        }

        public event EventHandler<GenericEventArgs<string>> OnRequestContentAvailable
        {
            add { _onRequestContentAvailable += value; }
            remove { _onRequestContentAvailable -= value; }
        }

        public event EventHandler<GenericEventArgs<string, byte[]>> OnResponseContentAvailable
        {
            add { _onResponseContentAvailable += value; }
            remove { _onResponseContentAvailable -= value; }
        }

        public event EventHandler<GenericEventArgs<HttpRequestMessage>> OnRequestAvailable
        {
            add { _onRequestAvailable += value; }
            remove { _onRequestAvailable -= value; }
        }

    }
}
