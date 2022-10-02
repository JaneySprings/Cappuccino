using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;

public class ValidationCallback: IValidationCallback { 
    public bool DebugLog { get; set; }
    public string? Message { get; private set; }

    public void OnValidationFail(string reason) {
        Message = reason;
        
        if (DebugLog) {
            Console.WriteLine(reason);
        }
    }
}