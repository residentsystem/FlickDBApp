using MailKit.Net.Smtp;
using MimeKit;

namespace FlickDBLib.Mail;

public class MailService
{
    public MailService(Mail mail)
    {
        _mail = mail;
    }

    Mail _mail;

    public void SendConfirmationEmail()
    {
        // Process Email

        //Instantiate a new MimeMessage
        var message = new MimeMessage();

        //Set the primary recipient of the message
        message.To.Add(new MailboxAddress($"{_mail.Firstname} + {_mail.Lastname}", _mail.Email));

        //Set the primary sender of the message
        message.From.Add(new MailboxAddress(_mail.SenderName, _mail.SenderAddress));

        //Set the subject of the message 
        message.Subject = _mail.Subject + $" - {_mail.Groupname}";

        //Set the body of the message
        message.Body = new TextPart("html")
        {
            Text = "<meta http-equiv='Content-Type' content='text/html; charset=Windows-1252'>" +
            "<div style='width:100%; background-color:#1968b7; padding-bottom:5%; padding-top:5%'>" +
                "<div style='background-color:#ffffff; align-content:center; width:60%; margin-left:auto; margin-right:auto'>" +
                    "<div style='align-content:center'>" +
                        "<img src='http://raftingmomentum.com/wpress/wp-content/uploads/2019/08/invitationEmail_fr.jpeg' style='width:100%; height:10%'>" +
                    "</div>" +
                    "<div style='text-align:center; margin-left:auto; margin-right:auto; font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 20px; font-weight:bold; color:#535353'>" +
                        $"<h1>Félicitations, votre groupe de Rafting est {_mail.Groupname}</h1>" +
                    "</div>" +
                    "<div style='color:#535353; font-family:Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif; font-size: 18px; padding-left:10%; padding-right:10%; text-align: center; width:80%'>" +
                        $"<p>Bonjour {_mail.Firstname},</p>" +
                            
                            $"<p>juste un petit mot pour vous dire que votre réservation avec Rafting Momentum est maintenant officielle! Votre paiement de {_mail.Totalgroup} $ a été accepté. Vous recevrez d'ici peu une facture confirmant votre paiement. Veuillez communiquez avec nous si vous n'avez rien reçu dans les 5 prochains jours.</p>" +

                            $"<p>Voici le lien que vous pouvez partager avec vos amis pour les inviter à venir avec vous en Rafting. Ce lien les apportera directement sur la page sommaire du forfait Rafting. Vous pouvez également utiliser ce lien pour faire un autre paiement : <a href='https://localhost:7116/adventureinvite/?groupuid={_mail.GroupUID}' style='color: #0000ff; font-weight: 600' target='_blank'>Votre groupe</a></p>" +

                            "<p>Si d'autres personnes désirent s'ajouter à votre groupe, et qu'il est impossible pour eux de réserver pour une raison ou une autre (notre système de réservation en ligne a certaines limites), dites-leur de communiquer avec nous et nous tenterons de les accommoder.</p>" +

                            "<p>Voir notre site web pour l'adresse (au bas de chaque page) :<br><a href='https://www.raftingmomentum.com' style='color:#0000ff; font-weight: 600' target='_blank'>raftingmomentum.com</a></p>" +

                            "<p>Il est important d'arriver à l'heure. <a href='http://raftingmomentum.com/politiquereservation' style='color: #0000ff; font-weight: 600' target='_blank'>Cliquez ici pour consulter nos politiques.</a></p>" +

                            "<p>Nous vous encourageons à aller sur notre site web section <a href='https://www.raftingmomentum.com' style='color: #0000ff; font-weight: 600' target='_blank'>« Informations »</a> pour prendre connaissances de notre" + 
                            "<a href='https://www.raftingmomentum.com/categorie-produit/hebergements' style='color: #0000ff; font-weight: 600' target='_blank'>« Demande de participation »</a>, nos" + 
                            "<a href='http://raftingmomentum.com/politiquereservation' style='color: #0000ff; font-weight: 600' target='_blank'>« Politiques de réservations »</a> ainsi que de notre document pouvant vous aider à ne rien oublier" + 
                            "lors de votre séjour chez nous <a href='https://www.raftingmomentum.com/informations' style='color: #0000ff; font-weight: 600' target='_blank'>« Feuille d'organisation - Choses à emporter »</a>.</p>" +

                            "<p style='font-weight: 600; text-decoration: underline'>Svp, nous faire part d'allergies alimentaires grave parmi votre groupe.</p>" +

                            "<p>D'ici là, si vous avez des questions, on vous invite à communiquer avec nous par courriel <a href='mailto:info@raftingmomentum.com' target='_blank'>info@raftingmomentum.com</a> ou par téléphone 819.360.8247 ou 1.800.690.RAFT.</p>" +

                            "<p style='font-weight: 600; text-decoration: underline'>L'activité a lieu beau temps mauvais temps.</p>" +

                            "<p style='color: red; font-weight: 600'>Merci de nous permettre de vivre notre passion!<br>À très bientôt :)</p>" +

                            "<p style='color:#0000ff; font-weight: 600' >Nous ne facturons pas de frais de service. Le guide de Raft aime son travail et le fait avec passion. Vous avez aimé votre journée Votre guide appréciera grandement un montant en guise de pourboire. Tout montant est apprécié.</p>" +                    
                    "</div>" +

                    "<div style='font-size: 14px; height: 200px'>" +
                        "<div style='float:left; margin-left:5%; width:40%'>" +
                           "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>1041, Route 148</div>" +
                           "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>Bryson, Quebec J0X 1H0<br>C.P. 39</div>" +
                           "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>info@raftingmomentum.com</div>" +
                        "</div>" +
                        "<div style='float:right; margin-right:5%; width:40%'>" +
                            "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>Outaouais - Ottawa</div>" +
                            "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>819-360-8247<br>Sans frais | Toll Free </div>" +
                            "<div style='background-color:#f2f2f2; margin:1%; padding: 1%'>1-800-690-RAFT (7238)</div>" +
                        "</div>" +
                       "<div style='clear: both'></div>" +
                    "</div>" +
                "</div>" +
            "</div>"      

        };

        //Connect to the online mail service and send the message
        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect(_mail.Host, _mail.Port, MailKit.Security.SecureSocketOptions.None);
            emailClient.Authenticate(_mail.Username, _mail.Password);
            emailClient.Send(message);
            emailClient.Disconnect(true);
        }
    }
}