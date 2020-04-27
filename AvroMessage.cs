using System;

namespace POC_kafka
{
    public class AvroMessage
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ClientDocument { get; set; }
        public string ClientName { get; set; }
        public string GuidParentFile { get; set; }
        public int? ExternalId { get; set; }
        public string Process { get; set; }
        public string ProcessName { get; set; }
        public int? ProcessId { get; set; }
        public string Direction { get; set; }
        public string Content { get; set; }


        // public void Create(AvroMessage avroMessage)
        // {
        //     this.Id = avroMessage.Id;
        //     this.FileName = avroMessage.FileName;
        //     this.ClientDocument = avroMessage.ClientDocument;
        //     this.ClientName = avroMessage.ClientName;
        //     this.GuidParentFile = avroMessage.GuidParentFile;
        //     this.ExternalId = avroMessage.ExternalId;
        //     this.Process = avroMessage.Process;
        //     this.ProcessName = avroMessage.ProcessName;
        //     this.ProcessId = avroMessage.ProcessId;
        //     this.Direction = avroMessage.Direction;
        //     this.Content = avroMessage.Content;
        // }

        public void Create()
        {
            this.Id = 6;
            this.FileName = "OCOREN_02343801_27022020_101800_001.txt";
            this.ClientDocument = "65849838000108";
            this.ClientName = "Transportadoras";
            this.GuidParentFile = null;
            this.ExternalId = 0;
            this.Process = "OcorrenciaVAN-Ver5.0";
            this.ProcessName = "Ocorrência";
            this.ProcessId = 1;
            this.Direction = "E";
            this.Content = "000SUDOESTE TRANSPORTES EIRELI - EPP  PENSKE LOGISTICS DO BRASIL LTDA - O2702201017270220101748                                                                                                                                                                                                            \r\n54027022020100513                                                                                                                                                                                                                                                                                          \r\n54102343801000185SUDOESTE TRANSPORTES EIRELI - EPP                                                                                                                                                                                                                                                         \r\n5420410411700114832 00029399021627022020093900PENSKE.594681                                                                                  18888                  00000000000000000000000000000000000000000000000000000000000000   000000000PENSKE/NIS.04104117001148_32_20_02_21_293990                 \r\n5420106957300064911 00142193000121022020100000PENSKE.590844                                                                                  16637                  00000000000000000000000000000000000000000000000000000000000000   000000000PENSKE/REN.01069573000649_11_20_02_17_1421930                \r\n5420106957300064911 00142312100121022020100000PENSKE.590844                                                                                  16637                  00000000000000000000000000000000000000000000000000000000000000   000000000PENSKE/REN.01069573000649_11_20_02_18_1423121                \r\n5420106957300064911 00142849521627022020094400PENSKE.595676                                                                                  17182                  00000000000000000000000000000000000000000000000000000000000000   000000000PENSKE/REN.01069573000649_11_20_02_22_1428495                \r\n5490004                                                                                                                                                                                                                                                                                                    \r\n";
        }

    }
}
