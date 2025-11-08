from abc import ABC, abstractmethod

class TV:
    def on(self):
        print("TV включен")

    def off(self):
        print("TV выключен")

    def set_channel(self, channel):
        print(f"Выбран канал {channel}")


class AudioSystem:
    def on(self):
        print("Аудиосистема включена")

    def off(self):
        print("Аудиосистема выключена")

    def set_volume(self, level):
        print(f"Громкость установлена на {level}")


class KinoPoisk:
    def open(self):
        print("Открыт сайт Кинопоиск")

    def play_movie(self, movie):
        print(f"Начат просмотр фильма: {movie}")

    def stop_movie(self):
        print("Просмотр фильма остановлен")


class GameConsole:
    def on(self):
        print("Игровая консоль включена")

    def start_game(self, game):
        print(f"Запущена игра: {game}")


class HomeTheaterFacade:
    def __init__(self, tv, audio, kinopoisk, console):
        self.tv = tv
        self.audio = audio
        self.kinopoisk = kinopoisk
        self.console = console

    def watch_movie(self, movie):
        print("\nПодготовка к просмотру фильма:")
        self.tv.on()
        self.audio.on()
        self.audio.set_volume(10)
        self.kinopoisk.open()
        self.kinopoisk.play_movie(movie)
        print("Фильм запущен")

    def end_movie(self):
        print("\nЗавершение просмотра:")
        self.kinopoisk.stop_movie()
        self.audio.off()
        self.tv.off()
        print("Система выключена")

    def play_game(self, game):
        print("\n Подготовка к игре: ")
        self.console.on()
        self.audio.on()
        self.audio.set_volume(7)
        self.console.start_game(game)
        print("Игра запущена")

    def listen_music(self):
        print("\n Прослушивание музыки:")
        self.tv.on()
        self.audio.on()
        self.audio.set_volume(6)
        print("Музыка играет")

class FileSystemComponent(ABC):
    @abstractmethod
    def display(self, indent=0):
        pass

    @abstractmethod
    def get_size(self):
        pass


class File(FileSystemComponent):
    def __init__(self, name, size):
        self.name = name
        self.size = size

    def display(self, indent=0):
        print(" " * indent + f"Файл: {self.name} ({self.size} КБ)")

    def get_size(self):
        return self.size


class Directory(FileSystemComponent):
    def __init__(self, name):
        self.name = name
        self.children = []

    def add(self, component):
        if component not in self.children:
            self.children.append(component)
        else:
            print(f"{component.__class__.__name__} уже существует в {self.name}")

    def remove(self, component):
        if component in self.children:
            self.children.remove(component)
        else:
            print(f"{component.__class__.__name__} не найдено в {self.name}")

    def display(self, indent=0):
        print(" " * indent + f"[Папка] {self.name}")
        for child in self.children:
            child.display(indent + 4)

    def get_size(self):
        total = sum(child.get_size() for child in self.children)
        return total


if __name__ == "__main__":
    # --- ФАСАД ---
    tv = TV()
    audio = AudioSystem()
    kinopoisk = KinoPoisk()
    console = GameConsole()

    home_theater = HomeTheaterFacade(tv, audio, kinopoisk, console)

    home_theater.watch_movie("Интерстеллар")
    home_theater.play_game("GTA V")
    home_theater.listen_music()
    home_theater.end_movie()

    print("\n")
    print("       ФАЙЛОВАЯ СИСТЕМА")
    print("")

    root = Directory("Главная")
    docs = Directory("Документы")
    pics = Directory("Картинки")

    file1 = File("Реферат.docx", 120)
    file2 = File("Отчет.pdf", 80)
    file3 = File("Фото1.jpg", 200)
    file4 = File("Фото2.png", 180)

    docs.add(file1)
    docs.add(file2)
    pics.add(file3)
    pics.add(file4)

    root.add(docs)
    root.add(pics)

    print("\n Содержимое файловой системы ")
    root.display()
    print(f"\nОбщий размер: {root.get_size()} КБ")
