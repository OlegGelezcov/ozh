using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using static System.Console;

namespace OzhConsole {

    class Program {

        static void Main(string[] args) {

            /*
            var oneSec = TimeSpan.FromSeconds(1);
            IObservable<long> ticks = Observable.Interval(oneSec);
            ticks.Trace("ticks");
            ReadLine();*/

            /*
            var inputs = new Subject<string>();
            using(inputs.Trace("inputs")) {
                for(string input; (input = ReadLine()) != "q";) {
                    inputs.OnNext(input);
                }
                inputs.OnCompleted();
            }*/

            /*
            IObservable<string> justHello = Observable.Return("hello");
            justHello.Trace("justHEllo");*/

            /*
            Observable.FromAsync(() => GetRate()).Trace("singleUsdEur");
            ReadLine();*/

            /*
            IEnumerable<char> e = new[] { 'a', 'b', 'c' };
            IObservable<char> chars = e.ToObservable();
            chars.Trace("chars");
            ReadLine();*/

            var oneSec = TimeSpan.FromSeconds(1);
            var ticks = Observable.Interval(oneSec);

            ticks.Select(n => n * 10).Trace("ticksX10");
            ReadKey();
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
