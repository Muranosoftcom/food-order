﻿using System.ComponentModel;

namespace WebUI.Areas.Admin.Models {
    public class SyncViewModel {

        [DisplayName("Синхронизация обедов")]
        public string SyncResult { get; set; }
        
        [DisplayName("Отправить нотификацию")]
        public string SendNotification { get; set; }
    }
}
