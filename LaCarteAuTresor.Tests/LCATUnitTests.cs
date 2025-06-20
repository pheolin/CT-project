using LaCarteAuTresor;

namespace LaCarteAuTresorTests;

public class LCATUnitTests
{
    const string INPUT_FILE_TEST = "TestFiles/InputFileTest.txt";
    const string OUTPUT_FILE_TEST = "TestFiles/OutputFileTest.txt";

    [Fact]
    public void GetAllFileLinesTest()
    {
        var adventure = new Adventure(INPUT_FILE_TEST, OUTPUT_FILE_TEST);
        var lines = adventure.GetAllFileLines(INPUT_FILE_TEST);
        var expectedLines = new[] { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3", "A - Lara - 1 - 1 - S - AADADAGGA" };


        for (var lineIndex = 0; lineIndex <= expectedLines.Length; lineIndex++)
            Assert.Equal(expectedLines[lineIndex], lines[lineIndex]);
    }

    [Fact]
    public void FeedOutputFileTest()
    {
        var adventure = new Adventure("", OUTPUT_FILE_TEST);

        //var timestamp = DateTime.Now.Ticks;

        var map = new Map(3, 4);
        SetFinalMapCasesSample(map);

        adventure.FeedOutputFile(OUTPUT_FILE_TEST, map);

        var fileLines = File.ReadAllLines(OUTPUT_FILE_TEST);
        var expectedFileLines = File.ReadAllLines(OUTPUT_FILE_TEST);

        for (var lineIndex = 0; lineIndex <= expectedFileLines.Length; lineIndex++)
        {
            Assert.Equal(expectedFileLines[lineIndex], fileLines[lineIndex]);
        }
    }

    [Fact]
    //Map LoadMap()
    public void LoadMapTest()
    {
        var adventure = new Adventure(INPUT_FILE_TEST, OUTPUT_FILE_TEST);
        var map = adventure.LoadMap();

        var expectedMap = new Map(3, 4);


        SetMapCasesSample(expectedMap.MapCases);

        for (int caseIndex = 0; caseIndex < 5; caseIndex++)
        {
            var expectedCase = expectedMap.MapCases[caseIndex];
            var case_ = map.MapCases[caseIndex];

            CasesEqualAsserting(expectedCase, case_);
        }

        Assert.Equal(expectedMap.HorizontalLength, map.HorizontalLength);
        Assert.Equal(expectedMap.VerticalLength, map.VerticalLength);
    }

    [Fact]
    //Map GetMap(string[] fileLines)
    public void GetMapTest()
    {
        var adventure = new Adventure(INPUT_FILE_TEST, OUTPUT_FILE_TEST);
        var map = adventure.GetMap(adventure.GetAllFileLines(INPUT_FILE_TEST));
        var expectedMap = GetMapSample();

        Assert.Equal(expectedMap.HorizontalLength, map.HorizontalLength);
        Assert.Equal(expectedMap.VerticalLength, map.VerticalLength);
    }



    [Fact]
    //Case GetCase(string fileLine, int lineNumber)
    public void GetCaseTest()
    {
        var adventure = new Adventure(INPUT_FILE_TEST, OUTPUT_FILE_TEST);
        var map = GetMapSample();

        for (var inputLineIndex = 0; inputLineIndex < 5; inputLineIndex++)
        {
            var case_ = adventure.GetCase(INPUT_FILE_TEST, inputLineIndex + 1);
            CasesEqualAsserting(map.MapCases[inputLineIndex], case_);
        }
    }

    private void SetMapCasesSample(List<Case> cases)
    {
        var M1Case = new Mountain(1, 0);
        var M2Case = new Mountain(2, 1);
        var T1Case = new TreasureLocation(0, 3, 2);
        var T2Case = new TreasureLocation(1, 3, 3);
        var ACase = new Adventurer(1, 1, "Lara", 'S', "AADADAGGA", 5);


        cases.Add(M1Case);
        cases.Add(M2Case);
        cases.Add(T1Case);
        cases.Add(T2Case);
        cases.Add(ACase);
    }

    void SetFinalMapCasesSample(Map map)
    {
        map.MapCases.Add(new Mountain(1, 0));
        map.MapCases.Add(new Mountain(2, 1));
        map.MapCases.Add(new TreasureLocation(1, 3, 2));
        var adventurer = new Adventurer(0, 3, "Lara", 'S', string.Empty, 5);
        adventurer.CollectedTreasures = 3;
        map.MapCases.Add(adventurer);
    }

    private Map GetMapSample()
    {
        var map = new Map(3, 4);
        SetMapCasesSample(map.MapCases);

        return map;
    }

    private Map GetFinalMapSample()
    {
        var map = new Map(3, 4);
        SetFinalMapCasesSample(map);

        return map;
    }
    private void CasesEqualAsserting(Case expectedCase, Case case_)
    {
        Assert.Equal(expectedCase.HorizontalOffset, case_.HorizontalOffset);
        Assert.Equal(expectedCase.VerticalOffset, case_.VerticalOffset);

        switch (case_)
        {
            case TreasureLocation treasureLocation:
                var expectedTreasureLocation = expectedCase as TreasureLocation;

                Assert.Equal(expectedTreasureLocation.RemainingTreasure, treasureLocation.RemainingTreasure);
                break;
            case Adventurer adventurer:
                var expectedAdventurer = expectedCase as Adventurer;

                Assert.Equal(expectedAdventurer.Moves, adventurer.Moves);
                Assert.Equal(expectedAdventurer.Name, adventurer.Name);
                Assert.Equal(expectedAdventurer.Direction, adventurer.Direction);
                Assert.Equal(expectedAdventurer.CollectedTreasures, adventurer.CollectedTreasures);

                break;
        }


    }
    private void MapEqualAsserting(Map map, Map expectedMap)
    {
        Assert.Equal(map.HorizontalLength, expectedMap.HorizontalLength);
        Assert.Equal(map.VerticalLength, expectedMap.VerticalLength);

        for (int caseIndex = 0; caseIndex < expectedMap.MapCases.Count; caseIndex++)
        {
            var expectedCase = expectedMap.MapCases[caseIndex];
            var case_ = map.MapCases[caseIndex];

            CasesEqualAsserting(expectedCase, case_);
        }
    }
    [Fact]
    public void MovesProcessingTest()
    {
        //MovesProcessing(Map map)
        var adventure = new Adventure(INPUT_FILE_TEST, OUTPUT_FILE_TEST);
        var map = GetMapSample();
        var finalMap = GetFinalMapSample();
        adventure.MovesProcessing(map);

        MapEqualAsserting(map, finalMap);
    }
}
