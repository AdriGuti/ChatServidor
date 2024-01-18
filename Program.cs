using ChatServidor;
using System.Net;
using System.Text;

IniciaServidor();
Console.WriteLine("Servidor iniciado");
Console.ReadLine();

async static void IniciaServidor()
{
    const int HTTP_TOT_OK = 200;
    const int HTTP_ERROR = 500;

    HttpListener listener = new HttpListener();
    listener.Prefixes.Add("http://*:51111/");
    listener.Start();

    string msg = "Error";
    int codiError = HTTP_ERROR;
    Chat chat = new Chat();

    while (true)
    {
        HttpListenerContext context = listener.GetContext();
        string url = context.Request.RawUrl.ToLower();
        Console.WriteLine(context.Request.RawUrl);
        try
        {
            if (url.StartsWith("/o"))
            {
                var missatges = chat.obtenirMissatges();
                msg = missatges.ToString();
                codiError = HTTP_TOT_OK;
            }
            else if (url.StartsWith("/a"))
            {
                string? missatge = context.Request.QueryString["m"];
                if (missatge != null)
                {
                    if (missatge.Contains("/clear"))
                        chat.clear();
                    else
                        chat.afegirMissatge(missatge);
                    msg = "ok";
                    codiError = HTTP_TOT_OK;
                }
                else
                    throw new Exception("Falten paràmetres");
            }
        }
        catch (Exception e)
        {
            msg = e.Message;
            Console.WriteLine(e.Message);
        }
        escriureResposta(context, msg, codiError);
    }
}
async static void escriureResposta(HttpListenerContext context, string msg, int codiError)
{
    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
    context.Response.StatusCode = codiError;
    context.Response.ContentEncoding = Encoding.UTF8;
    using (Stream s = context.Response.OutputStream)
    using (StreamWriter writer = new StreamWriter(s))
    {
        writer.WriteAsync(msg);
    }
}
