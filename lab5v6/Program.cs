using System;
using System.Collections.Generic;
using System.Linq;

// ====== КЛАСИ ======

public class Team
{
    public string Name { get; }
    public int Wins { get; private set; }
    public int Draws { get; private set; }
    public int Losses { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }

    public int Points => Wins * 3 + Draws;
    public int GoalDifference => GoalsFor - GoalsAgainst;

    public Team(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Назва команди не може бути порожньою.");

        Name = name;
    }

    public void RecordMatch(int goalsFor, int goalsAgainst)
    {
        GoalsFor += goalsFor;
        GoalsAgainst += goalsAgainst;

        if (goalsFor > goalsAgainst) Wins++;
        else if (goalsFor == goalsAgainst) Draws++;
        else Losses++;
    }

    public override string ToString()
    {
        return $"{Name,-15} | Очки: {Points,2} | В: {Wins} | Н: {Draws} | П: {Losses} | Різн.: {GoalDifference}";
    }
}

// ====== МАТЧ ======

public class Match
{
    public Team TeamA { get; }
    public Team TeamB { get; }
    public int ScoreA { get; }
    public int ScoreB { get; }

    public Match(Team teamA, Team teamB, int scoreA, int scoreB)
    {
        if (teamA == teamB)
            throw new InvalidMatchException("Матч не може бути зігранний між однією і тією ж командою!");

        TeamA = teamA;
        TeamB = teamB;
        ScoreA = scoreA;
        ScoreB = scoreB;

        TeamA.RecordMatch(scoreA, scoreB);
        TeamB.RecordMatch(scoreB, scoreA);
    }

    public override string ToString()
    {
        return $"{TeamA.Name} {ScoreA}:{ScoreB} {TeamB.Name}";
    }
}

// ====== ВЛАСНИЙ ВИНЯТОК ======

public class InvalidMatchException : Exception
{
    public InvalidMatchException(string message) : base(message) { }
}

// ====== GENERIC РЕПОЗИТОРІЙ ======

public interface IRepository<T>
{
    void Add(T item);
    bool Remove(Predicate<T> match);
    IEnumerable<T> Where(Func<T, bool> predicate);
    T? FirstOrDefault(Func<T, bool> predicate);
    IReadOnlyList<T> All();
}

public class Repository<T> : IRepository<T>
{
    private readonly List<T> _data = new();

    public void Add(T item) => _data.Add(item);
    public bool Remove(Predicate<T> match) => _data.RemoveAll(match) > 0;
    public IEnumerable<T> Where(Func<T, bool> predicate) => _data.Where(predicate);
    public T? FirstOrDefault(Func<T, bool> predicate) => _data.FirstOrDefault(predicate);
    public IReadOnlyList<T> All() => _data;
}

// ====== ТУРНІР ======

public class Tournament
{
    public string Name { get; }
    private readonly IRepository<Team> _teams;
    private readonly List<Match> _matches = new();

    public Tournament(string name, IRepository<Team> teamRepo)
    {
        Name = name;
        _teams = teamRepo;
    }

    public void AddTeam(Team team) => _teams.Add(team);

    public void AddMatch(Team teamA, Team teamB, int scoreA, int scoreB)
    {
        var match = new Match(teamA, teamB, scoreA, scoreB);
        _matches.Add(match);
    }

    public void PrintResults()
    {
        Console.WriteLine($"\nТурнір: {Name}");
        Console.WriteLine(new string('=', 50));

        var table = _teams.All()
            .OrderByDescending(t => t.Points)
            .ThenByDescending(t => t.GoalDifference);

        foreach (var t in table)
            Console.WriteLine(t);

        Console.WriteLine(new string('-', 50));
        Console.WriteLine("Матчі:");
        foreach (var m in _matches)
            Console.WriteLine(m);
    }
}

// ====== ПРОГРАМА ======

public class Program
{
    public static void Main()
    {
        try
        {
            var teamRepo = new Repository<Team>();
            var tournament = new Tournament("Настільні ігри 2025", teamRepo);

            // Додавання команд
            var t1 = new Team("Кобра");
            var t2 = new Team("Вогонь");
            var t3 = new Team("Молнія");

            tournament.AddTeam(t1);
            tournament.AddTeam(t2);
            tournament.AddTeam(t3);

            // Додавання матчів
            tournament.AddMatch(t1, t2, 2, 1);
            tournament.AddMatch(t2, t3, 1, 1);
            tournament.AddMatch(t3, t1, 0, 3);

            // Перевірка помилки
            // tournament.AddMatch(t1, t1, 1, 0); // Викличе InvalidMatchException

            tournament.PrintResults();
        }
        catch (InvalidMatchException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }
    }
}
