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
    /// <summary>
    /// Manages HTTPClient
    /// </summary>
    public class LoggingHandler : DelegatingHandler
    {
        /// <summary>
        /// Constructor for Delegating /LoggingHandler for HTTPClient
        /// </summary>
        /// <param name="innerHandler"></param>
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        private event EventHandler<GenericEventArgs<HttpRequestMessage>> _onRequestAvailable;
        private event EventHandler<GenericEventArgs<string>> _onRequestContentAvailable;
        private event EventHandler<GenericEventArgs<string,byte[]>> _onResponseContentAvailable;
        /// <summary>
        /// SendAsynd HTTP Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
                try
                {
                    if(response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("request was a success");
                    }
                    else
                    {
                        Console.WriteLine("response failed, httpstatus: "+response.StatusCode);
                    }
                    Console.WriteLine(response.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Response shows Request failed");
                    Console.WriteLine(ex.ToString());
                }
                
                if (response.Content != null)
                {
                    if (_onResponseContentAvailable != null)
                        _onResponseContentAvailable(this, new GenericEventArgs<string, byte[]>(request.GetHashCode().ToString(), await response.Content.ReadAsByteArrayAsync()));
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                if(response!=null)
                {
                    Console.WriteLine($"Response: Success? => {response.IsSuccessStatusCode}");
                    Console.WriteLine($"Response: HttpStatuscode? => {response.StatusCode}");
                    Console.WriteLine($"RequestMessageToResponse? => {response.RequestMessage}");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    Console.WriteLine();
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()) ;
                return null;
            }
            
        }
        /// <summary>
        /// Generic EventHandler OnRequestContentAvailable
        /// </summary>

        public event EventHandler<GenericEventArgs<string>> OnRequestContentAvailable
        {
            add { _onRequestContentAvailable += value; }
            remove { _onRequestContentAvailable -= value; }
        }
        /// <summary>
        /// Generic Eventhandler OnResponseContentAvailable
        /// </summary>
        public event EventHandler<GenericEventArgs<string, byte[]>> OnResponseContentAvailable
        {
            add { _onResponseContentAvailable += value; }
            remove { _onResponseContentAvailable -= value; }
        }
        /// <summary>
        /// Generic Eventhandler OnRequestAvailable
        /// </summary>
        public event EventHandler<GenericEventArgs<HttpRequestMessage>> OnRequestAvailable
        {
            add { _onRequestAvailable += value; }
            remove { _onRequestAvailable -= value; }
        }

    }
}
