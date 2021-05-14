using System.Collections.Generic;
using CluedIn.Crawling.Greenhouse.Core;

namespace CluedIn.Crawling.Greenhouse.Integration.Test
{
  public static class GreenhouseConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { GreenhouseConstants.KeyName.ApiKey, "demo" } //cea405ac4796defb9ab7dea89f8f09fb
            };
    }
  }
}
