using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPushNotification.Class
{
    ///<Summary>
    /// Clase de la Config de la Notificacion
    ///</Summary>
    ///
    public class WebPushNotificationConfig
    {
        ///<Summary>
        /// Subject
        ///</Summary>
        ///
        public string Subject { get; set; }

        ///<Summary>
        /// Clave Publica
        ///</Summary>
        ///
        public string PublicKey { get; set; }

        ///<Summary>
        /// Clave Privada
        ///</Summary>
        ///
        public string PrivateKey { get; set; }
    }
}
