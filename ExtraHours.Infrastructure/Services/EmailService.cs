using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
public class EmailService
{
    public async Task SendCodeForEmailAsync(string email, string name, string code)
    {
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("Horas extras - Amadeus", "tucorreo@gmail.com"));
        mensaje.To.Add(new MailboxAddress(name, email));
        mensaje.Subject = "Tu Código Empresarial";
        mensaje.Body = new TextPart("plain")
        {
            Text = $"Hola {name},\n\nTu código empresarial es: {code}\n\n¡Bienvenido a la empresa!"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await client.SendAsync(mensaje);
        await client.DisconnectAsync(true);
    }
}
