
using GeneratorData.Model;
using System.Collections.Generic;

public class GenerationOutput
{

    private List<GenerationOutputGenerator> totals;

    private List<GenerationOutputDay> maxEmissionGenerators;

    private List<GenerationOutputActualHeatRates> actualHeatRatesField;

    public GenerationOutput()
    {
        totals = new List<GenerationOutputGenerator> ();
        maxEmissionGenerators = new List<GenerationOutputDay> ();   
        actualHeatRatesField = new List<GenerationOutputActualHeatRates>();   
    }

    public List<GenerationOutputGenerator> Totals
    {
        get => this.totals;
        set => this.totals = value;
    }

    public List<GenerationOutputDay> MaxEmissionGenerators
    {
        get => this.maxEmissionGenerators;
        set => this.maxEmissionGenerators = value;
    }

    public List<GenerationOutputActualHeatRates> ActualHeatRates
    {
        get => this.actualHeatRatesField;
        set => this.actualHeatRatesField = value;
    }
}


