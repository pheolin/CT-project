using System;
using System.IO;

namespace LaCarteAuTresor
{
    public class Adventure
    {
        string _inputFilepath;
        string _outputFilepath;

        int _referenceLetterPosition = 0;
        string _separator = " - ";
        public Adventure(string InputFilepath, string OutputFilepath)
        {
            _inputFilepath = InputFilepath;
            _outputFilepath = OutputFilepath;
        }

        internal string[] GetAllFileLines(string inputFilePath)
        {
            if (string.IsNullOrWhiteSpace(inputFilePath))
                throw new ArgumentException("the input filename path can't be empty.", nameof(inputFilePath));

            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("file not found.", inputFilePath);

            return File.ReadAllLines(inputFilePath);
        }

        public void Play()
        {
            var map = LoadMap();

            MovesProcessing(map);

            FeedOutputFile(_outputFilepath, map);
        }

        public void FeedOutputFile(string outputFilepath, Map map)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilepath))
                {
                    writer.WriteLine(string.Concat(Map.LetterReference, _separator, map.HorizontalLength, _separator, map.VerticalLength));

                    foreach (var mountain in map.Mountains)
                    {
                        writer.WriteLine(string.Concat(mountain.LetterReference, _separator, mountain.HorizontalOffset, _separator, mountain.VerticalOffset));
                    }

                    writer.WriteLine(@"# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}");
                    foreach (var treasureLocation in map.TreasureLocations)
                    {
                        if(treasureLocation.RemainingTreasure > 0)
                            writer.WriteLine(string.Concat(treasureLocation.LetterReference,
                            _separator,
                            treasureLocation.HorizontalOffset,
                            _separator,
                            treasureLocation.VerticalOffset,
                            _separator,
                            treasureLocation.RemainingTreasure)
                            );
                    }

                    writer.WriteLine(@"#{A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axevertical} - {Orientation} - {Nb. trésors ramassés}");
                    foreach (var adventurer in map.Adventurers)
                    {
                        writer.WriteLine(string.Concat(adventurer.LetterReference,
                        _separator,
                        adventurer.Name,
                        _separator,
                        adventurer.HorizontalOffset,
                        _separator,
                        adventurer.VerticalOffset,
                        _separator,
                        adventurer.Direction,
                        _separator,
                        adventurer.CollectedTreasures
                        ));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"File writting error : {ex.Message}");
            }
        }
        internal Map LoadMap()
        {
            var inputFileLines = GetAllFileLines(_inputFilepath);
            var map = GetMap(inputFileLines);

            var lineNumber = 0;
            foreach (var IFline in inputFileLines)
            {
                if (string.IsNullOrWhiteSpace(IFline) || IFline.StartsWith('#') || IFline.StartsWith('C'))
                    continue;
                map.MapCases.Add(GetCase(IFline, lineNumber++));
            }
            return map;
        }

        internal Map GetMap(string[] fileLines)
        {
            foreach (string IFLine in fileLines)
            {
                // if (string.IsNullOrWhiteSpace(IFLine) || IFLine.StartsWith('#'))
                //     continue;

                // On split sur le char '-' simplement
                string[] IFLineTab = IFLine
                    .Split('-', StringSplitOptions.RemoveEmptyEntries) // on supprime les vides au passage
                    .Select(part => part.Trim()) // on supprime les espaces autour
                    .ToArray();

                var hString = IFLineTab[Map.HorizontalLengthPosition];
                var vString = IFLineTab[Map.VerticalLengthPosition];
                var h = int.Parse(hString);
                var v = int.Parse(vString);

                if (char.Parse(IFLineTab[_referenceLetterPosition]) == Map.LetterReference)
                {
                    var map = new Map(h, v);
                    return map;
                }
            }

            throw new Exception("No map descritpion line in the file.");
        }
        internal Case GetCase(string fileLine, int lineNumber)
        {
            var firstLetter = fileLine.First();
            Case mapCase = new Case(0, 0);
            var splitedFileLine = fileLine.Split(" - ");

            switch (firstLetter)
            {
                case 'M':
                    mapCase = new Mountain(
                        int.Parse(splitedFileLine[Mountain.HorizontalLengthPosition]),
                        int.Parse(splitedFileLine[Mountain.VerticalLengthPosition]));
                    return mapCase;
                case 'T':
                    mapCase = new TreasureLocation(
                        int.Parse(splitedFileLine[TreasureLocation.HorizontalLengthPosition]),
                        int.Parse(splitedFileLine[TreasureLocation.VerticalLengthPosition]),
                        int.Parse(splitedFileLine[TreasureLocation.RemainingTreasurePosition])
                        );
                    return mapCase;
                case 'A':
                    mapCase = new Adventurer(
                        int.Parse(splitedFileLine[Adventurer.HorizontalLengthPosition]),
                        int.Parse(splitedFileLine[Adventurer.VerticalLengthPosition]),
                        splitedFileLine[Adventurer.NamePosition],
                        char.Parse(splitedFileLine[Adventurer.DirectionPosition]),
                        splitedFileLine[Adventurer.MovesPosition],
                        lineNumber
                    );
                    return mapCase;
            }

            throw new Exception($"No case description match for this line: {fileLine}");
        }

        internal void MovesProcessing(Map map)
        {
            var maxMoves = map.Adventurers.Select(adventurer => (adventurer as Adventurer).Moves.Length).Max();
            for (int moveNumber = 0; moveNumber < maxMoves; moveNumber++)
            {
                foreach (var adventurer in map.Adventurers)
                {
                    if (adventurer.Moves.Length > moveNumber)
                    {
                        var move = adventurer.Moves[moveNumber];
                        Case? potentialCase = null;
                        var nextDirection = adventurer.Direction;
                        switch (move)
                        {
                            case 'A':
                                switch (nextDirection)
                                {
                                    case 'N':
                                        if (adventurer.VerticalOffset > 0)
                                            potentialCase = new Case(adventurer.HorizontalOffset, adventurer.VerticalOffset - 1);
                                        break;
                                    case 'S':
                                        if (adventurer.VerticalOffset < map.VerticalLength)
                                            potentialCase = new Case(adventurer.HorizontalOffset, adventurer.VerticalOffset + 1);
                                        break;
                                    case 'O':
                                        if (adventurer.HorizontalOffset > 0)
                                            potentialCase = new Case(adventurer.HorizontalOffset - 1, adventurer.VerticalOffset);
                                        break;
                                    case 'E':
                                        if (adventurer.HorizontalOffset < map.HorizontalLength)
                                            potentialCase = new Case(adventurer.HorizontalOffset + 1, adventurer.VerticalOffset);
                                        break;
                                }
                                break;
                            case 'G':
                                switch (nextDirection)
                                {
                                    case 'N':
                                        nextDirection = 'O';
                                        break;
                                    case 'S':
                                        nextDirection = 'E';
                                        break;
                                    case 'O':
                                        nextDirection = 'S';
                                        break;
                                    case 'E':
                                        nextDirection = 'N';
                                        break;
                                }
                                break;
                            case 'D':
                                switch (nextDirection)
                                {
                                    case 'N':
                                        nextDirection = 'E';
                                        break;
                                    case 'S':
                                        nextDirection = 'O';
                                        break;
                                    case 'O':
                                        nextDirection = 'N';
                                        break;
                                    case 'E':
                                        nextDirection = 'S';
                                        break;
                                }
                                break;
                        }
                        adventurer.Direction = nextDirection;

                        //Move if the case is free
                        bool hasMoved = false;
                        if (potentialCase != null &&
                            !map.Mountains.Any(mountain => mountain.HorizontalOffset == potentialCase.HorizontalOffset
                                && mountain.VerticalOffset == potentialCase.VerticalOffset)
                            && !map.Adventurers.Any(otherAdventurer => otherAdventurer.HorizontalOffset == potentialCase.HorizontalOffset
                                && otherAdventurer.VerticalOffset == potentialCase.VerticalOffset)
                            )
                        {
                            adventurer.HorizontalOffset = potentialCase.HorizontalOffset;
                            adventurer.VerticalOffset = potentialCase.VerticalOffset;
                            hasMoved = true;
                        }

                        //Treasure found
                        var treasureLocation = map.TreasureLocations.FirstOrDefault(aTreasureLocation => aTreasureLocation.HorizontalOffset == adventurer.HorizontalOffset
                        && aTreasureLocation.VerticalOffset == adventurer.VerticalOffset);
                        if (treasureLocation != null && hasMoved)
                        {
                            treasureLocation.RemainingTreasure--;
                            adventurer.CollectedTreasures++;
                        }
                    }
                }
            }
        }

    }
}