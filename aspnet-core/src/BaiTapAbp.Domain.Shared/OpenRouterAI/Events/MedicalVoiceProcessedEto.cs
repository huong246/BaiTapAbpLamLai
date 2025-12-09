using System;
using System.Collections.Generic;

namespace Ord.MasterData.Services.OpenRouterAI.Events;
 [Serializable]
public class MedicalVoiceProcessedEto
{
    public bool Success { get; set; }
    public List<FieldData> Fields { get; set; }
    public MedicalVoiceProcessedEto()
    { 
        Fields = new List<FieldData>();
    } 
}
