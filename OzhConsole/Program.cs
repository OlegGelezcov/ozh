using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using static System.Console;

namespace OzhConsole {

    class Program {

        static void Main(string[] args) {
            new ContainerTests().Install();
        }

        static async Task<int> GetRate() {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return 3;
        }

    }

    public delegate (T Value, int Seed) Generator<T>(int seeed);

    public static class RxExt {
        public static IDisposable Trace<T>(this IObservable<T> source, string name)
            => source.Subscribe(
                onNext: t => WriteLine($"{name} -> {t}"),
                onError: ex => WriteLine($"{name} ERROR: {ex.Message}"),
                onCompleted: () => WriteLine($"{name} END"));
    }

}
