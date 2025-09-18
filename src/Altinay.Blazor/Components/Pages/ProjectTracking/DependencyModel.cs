using System;

namespace Altinay.Blazor.Components.Pages.ProjectTracking
{
    /// <summary>
    /// Bağımlılık türleri
    /// </summary>
    public enum DependencyType
    {
        FinishToStart,  // Sondan Başa (FS) - En yaygın
        StartToStart,   // Başlangıçtan Başlangıca (SS)
        FinishToFinish, // Sondan Sona (FF)
        StartToFinish   // Başlangıçtan Sona (SF)
    }

    /// <summary>
    /// Bağımlılık modeli
    /// </summary>
    public class DependencyModel
    {
        public string TaskId { get; set; } = string.Empty;
        public DependencyType Type { get; set; } = DependencyType.FinishToStart;
        public int Lag { get; set; } = 0; // Gec
    }
}
