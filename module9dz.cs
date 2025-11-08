using System;
using System.Collections.Generic;

class TV
{
    public void On() => Console.WriteLine("TV включен");
    public void Off() => Console.WriteLine("TV выключен");
    public void SetChannel(string channel) => Console.WriteLine($"Выбран канал {channel}");
}

class AudioSystem
{
    public void On() => Console.WriteLine("Аудиосистема включена");
    public void Off() => Console.WriteLine("Аудиосистема выключена");
    public void SetVolume(int level) => Console.WriteLine($"Громкость установлена на {level}");
}

class KinoPoisk
{
    public void Open() => Console.WriteLine("Открыт сайт Кинопоиск");
    public void PlayMovie(string movie) => Console.WriteLine($"Начат просмотр фильма: {movie}");
    public void StopMovie() => Console.WriteLine("Просмотр фильма остановлен");
}

class GameConsole
{
    public void On() => Console.WriteLine("Игровая консоль включена");
    public void StartGame(string game) => Console.WriteLine($"Запущена игра: {game}");
}

class HomeTheaterFacade
{
    private TV tv;
    private AudioSystem audio;
    private KinoPoisk kinopoisk;
    private GameConsole console;

    public HomeTheaterFacade(TV tv, AudioSystem audio, KinoPoisk kinopoisk, GameConsole console)
    {
        this.tv = tv;
        this.audio = audio;
        this.kinopoisk = kinopoisk;
        this.console = console;
    }

    public void WatchMovie(string movie)
    {
        Console.WriteLine("\n Подготовка к просмотру фильма ");
        tv.On();
        audio.On();
        audio.SetVolume(10);
        kinopoisk.Open();
        kinopoisk.PlayMovie(movie);
        Console.WriteLine("Фильм запущен");
    }

    public void EndMovie()
    {
        Console.WriteLine("\n Завершение просмотра ");
        kinopoisk.StopMovie();
        audio.Off();
        tv.Off();
        Console.WriteLine("Система выключена");
    }

    public void PlayGame(string game)
    {
        Console.WriteLine("\n Подготовка к игре ");
        console.On();
        audio.On();
        audio.SetVolume(7);
        console.StartGame(game);
        Console.WriteLine("Игра запущена");
    }

    public void ListenMusic()
    {
        Console.WriteLine("\n Прослушивание музыки ");
        tv.On();
        audio.On();
        audio.SetVolume(6);
        Console.WriteLine("Музыка играет");
    }
}


abstract class FileSystemComponent
{
    public abstract void Display(int indent = 0);
    public abstract int GetSize();
}

class File : FileSystemComponent
{
    private string name;
    private int size;

    public File(string name, int size)
    {
        this.name = name;
        this.size = size;
    }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"Файл: {name} ({size} КБ)");
    }

    public override int GetSize() => size;
}

class Directory : FileSystemComponent
{
    private string name;
    private List<FileSystemComponent> children = new List<FileSystemComponent>();

    public Directory(string name)
    {
        this.name = name;
    }

    public void Add(FileSystemComponent component)
    {
        if (!children.Contains(component))
            children.Add(component);
        else
            Console.WriteLine($"{component.GetType().Name} уже существует в {name}");
    }

    public void Remove(FileSystemComponent component)
    {
        if (children.Contains(component))
            children.Remove(component);
        else
            Console.WriteLine($"{component.GetType().Name} не найдено в {name}");
    }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"[Папка] {name}");
        foreach (var child in children)
        {
            child.Display(indent + 4);
        }
    }

    public override int GetSize()
    {
        int total = 0;
        foreach (var child in children)
            total += child.GetSize();
        return total;
    }
}

class Program
{
    static void Main()
    {
        TV tv = new TV();
        AudioSystem audio = new AudioSystem();
        KinoPoisk kinopoisk = new KinoPoisk();
        GameConsole console = new GameConsole();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audio, kinopoisk, console);

        homeTheater.WatchMovie("Интерстеллар");
        homeTheater.PlayGame("GTA V");
        homeTheater.ListenMusic();
        homeTheater.EndMovie();

        Console.WriteLine("       ФАЙЛОВАЯ СИСТЕМА");

        Directory root = new Directory("Главная");
        Directory docs = new Directory("Документы");
        Directory pics = new Directory("Картинки");

        File file1 = new File("Реферат.docx", 120);
        File file2 = new File("Отчет.pdf", 80);
        File file3 = new File("Фото1.jpg", 200);
        File file4 = new File("Фото2.png", 180);

        docs.Add(file1);
        docs.Add(file2);
        pics.Add(file3);
        pics.Add(file4);

        root.Add(docs);
        root.Add(pics);

        Console.WriteLine("\n Содержимое файловой системы ");
        root.Display();
        Console.WriteLine($"\nОбщий размер: {root.GetSize()} КБ");
    }
}

