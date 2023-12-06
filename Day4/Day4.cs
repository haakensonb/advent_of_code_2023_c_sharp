namespace AdventOfCode2023;

public record Card
{
    public int id = 0;

    public HashSet<int> winningNumbers = new();

    public HashSet<int> givenNumbers = new();

    public bool isCopy = false;

    private Card()
    {
    }

    public Card(string input)
    {
        var split = input.Split(" | ");

        var id = split[0].Split(":")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
        this.id = Int32.Parse(id);

        this.winningNumbers = split[0]
            .Split(": ")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => Int32.Parse(num))
            .ToHashSet();
        this.givenNumbers = split[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => Int32.Parse(num))
            .ToHashSet();
    }

    public static Card Copy(Card otherCard)
    {
        // Shallow copy?
        var newCard = new Card();
        newCard.id = otherCard.id;
        newCard.winningNumbers = otherCard.winningNumbers;
        newCard.givenNumbers = otherCard.givenNumbers;
        newCard.isCopy = true;
        return newCard;
    }

    public IEnumerable<int> GetIntersectingNumbers()
    {
        // Determine how many of the given numbers are winning numbers
        return this.winningNumbers.Intersect(this.givenNumbers);
    }

    public int CalcPoints()
    {
        var intersectNums = this.GetIntersectingNumbers();
        var n = intersectNums.Count();
        if (n > 0)
        {
            return (int)Math.Pow(2, n - 1);
        }
        return 0;
    }

    public List<int> CalcCardCopyIds()
    {
        var intersectNums = this.GetIntersectingNumbers();
        var copyIds = Enumerable.Range(this.id + 1, intersectNums.Count());
        return copyIds.ToList();
    }
}

class Day4
{
    public static string SolvePart1(string input)
    {
        var sum = 0;
        var lines = input.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var card = new Card(line);
            sum += card.CalcPoints();
        }
        return sum.ToString();
    }

    public static string SolvePart2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var cardIdToCount = new Dictionary<int, int>();
        var cardOriginals = new Dictionary<int, Card>();
        var cardQueue = new Queue<Card>();
        // Populate initial values
        foreach (var line in lines)
        {
            var card = new Card(line);
            cardQueue.Enqueue(card);
            cardOriginals[card.id] = card;
        }

        while (cardQueue.Any())
        {
            var card = cardQueue.Dequeue();
            // First, increment the count for the current card
            if (cardIdToCount.ContainsKey(card.id))
            {
                cardIdToCount[card.id] += 1;
            }
            else
            {
                cardIdToCount[card.id] = 1;
            }
            // Then, add any copies to the queue
            var copyIds = card.CalcCardCopyIds();
            foreach (var id in copyIds)
            {
                var cardCopy = Card.Copy(cardOriginals[id]);
                cardQueue.Enqueue(cardCopy);
            }
        }
        // Sum up all the card counts
        return cardIdToCount.Sum(x => x.Value).ToString();
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day4.SolvePart1(input);
        Console.WriteLine($"Day 4, part 1: {answerPart1}");
        var answerPart2 = Day4.SolvePart2(input);
        Console.WriteLine($"Day 4, part 2: {answerPart2}");
    }
}