using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SocketTest35
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();


            while (true)
            {
                Console.WriteLine("Listening...");
                HttpListenerContext ctx = listener.GetContext();

                //get request and respose
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                string data = "";
                if (req.HasEntityBody)

                {
                    using (System.IO.Stream body = req.InputStream)
                    {
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(body, req.ContentEncoding))
                        {
                            data = reader.ReadToEnd();
                        }
                    }



                    string responseString = "<HTML><BODY><h1>Hello world!</h1><p>" + data + "</p></BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                    // Get a response stream and write the response to it.
                    resp.ContentLength64 = buffer.Length;
                    System.IO.Stream output = resp.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();

                }
                
                //Console.WriteLine("Done - press any key");
                //Console.ReadLine();
            }
            // never gets here
            listener.Stop();

        }


    }
}
