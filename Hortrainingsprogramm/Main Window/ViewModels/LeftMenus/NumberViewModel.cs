using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class NumberViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isQuizCalled { get; set; }
        public override bool isPracticeCalled { get; set; } = false;

        private LinkedList<string> zahlList = new LinkedList<string>();
        private Random random = new();
        private Tuple<BigInteger, BigInteger> interval;
        private HashSet<BigInteger> zahlHashSet = new ();
        private BigInteger minWert, maxWert;

        public string NumberViewButtonTitle { get; set; }
        public string NumberViewlAllNumbersTitle { get; set; }
        public string OneDigitNumberTitle { get; set; }
        public string TwoDigitNumberTitle { get; set; }
        public string ThreeDigitNumberTitle { get; set; }
        public string ThousandDigitNumberTitle { get; set; }
        public string MillionDigitNumberTitle { get; set; }
        public string BillionDigitNumberTitle { get; set; }
        public string TrillionDigitNumberTitle { get; set; }
        public string QuadrillionDigitNumberTitle { get; set; }



        public NumberViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }



        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            if (eventArgs.Data is MenuEntryViewModel entryModel)
            {
                this.baseLanguage = entryModel.baseLanguage;
                this.NumberViewButtonTitle = baseLanguage.NumberViewButtonTitle;
                this.NumberViewlAllNumbersTitle = baseLanguage.NumberViewlAllNumbersTitle;
                this.OneDigitNumberTitle = baseLanguage.OneDigitNumberTitle;
                this.TwoDigitNumberTitle = baseLanguage.TwoDigitNumberTitle;
                this.ThreeDigitNumberTitle = baseLanguage.ThreeDigitNumberTitle;
                this.ThousandDigitNumberTitle = baseLanguage.ThousandDigitNumberTitle;
                this.MillionDigitNumberTitle = baseLanguage.MillionDigitNumberTitle;
                this.BillionDigitNumberTitle = baseLanguage.BillionDigitNumberTitle;
                this.TrillionDigitNumberTitle = baseLanguage.TrillionDigitNumberTitle;
                this.QuadrillionDigitNumberTitle = baseLanguage.QuadrillionDigitNumberTitle;

            }

        }


        public ICommand NavigateBack => new RelayCommand(parameter =>
        {

            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;

            }

            this.navigationService.NavigateTo(nameof(MenuEntryView), this);


        });


        public ICommand EinStelligCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("9");
            generateZahl(Tuple.Create(minWert, maxWert), true, false);


        });


        public ICommand ZweiStelligCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("10");
            maxWert = BigInteger.Parse("99");
            generateZahl(Tuple.Create(minWert, maxWert), true, false);

        });

        public ICommand DreiStelligCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("100");
            maxWert = BigInteger.Parse("999");
            generateZahl(Tuple.Create(minWert, maxWert), false, isStellig: true);

        });


        public ICommand ThousandCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000");
            generateZahl(Tuple.Create(minWert, maxWert), false, false);

        });


        public ICommand MillionCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000000");
            generateZahl(Tuple.Create(minWert, maxWert), false, false);

        });


        public ICommand BillionCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000000000");
            generateZahl(Tuple.Create(minWert, maxWert), false, false);

        });



        public ICommand TrillionCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000000000000");
            generateZahl(Tuple.Create(minWert, maxWert), false, false);


        });



        public ICommand QuadrillionCommand => new RelayCommand(parameter =>
        {

            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000000000000000");
            generateZahl(Tuple.Create(minWert, maxWert), false, false);


        });


        public ICommand GeschmischtCommand => new RelayCommand(parameter =>
        {
            minWert = BigInteger.Parse("0");
            maxWert = BigInteger.Parse("1000");
            generateZahl(Tuple.Create(minWert, maxWert), false,true);

        });



        private void generateZahl(Tuple<BigInteger, BigInteger> interval, bool isKlein, bool isStellig)
        {

            zahlList.Clear();
            zahlHashSet.Clear();

            if (isKlein)
            {
                for (BigInteger i = interval.Item1; i <= interval.Item2; i++)
                {
                    zahlList.AddLast(i.ToString());
                }

                Shuffle(zahlList);

                callPractice();

                return;
            }
            

            if (isStellig)
            {

                while (zahlHashSet.Count < 120)
                {
                    BigInteger rastgeleSayi = GenerateRandomBigInteger(interval.Item1, interval.Item2);
                    zahlHashSet.Add(rastgeleSayi);
                }


                foreach (var zahl in zahlHashSet)
                {
                    zahlList.AddLast(zahl.ToString());
                }

            }
            else
            {

                for (int i = 0; i < 3; i++)
                {
                    int counter = 0;
                    BigInteger min = interval.Item2 * BigInteger.Pow(10, i);
                    BigInteger max = interval.Item2 * BigInteger.Pow(10, i + 1);

                    while (counter < 40)
                    {
                        BigInteger randomNumber = GenerateRandomBigInteger(min, max);

                        if (!zahlHashSet.Contains(randomNumber))
                        {
                            zahlHashSet.Add(randomNumber);
                            counter++;
                        }
                    }
                }

                Shuffle(zahlList);

                foreach (var zahl in zahlHashSet)
                {
                    zahlList.AddLast(zahl.ToString());
                }
            }


            callPractice();


        }


    
        private BigInteger GenerateRandomBigInteger(BigInteger minValue, BigInteger maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue", "minValue cannot be greater than maxValue.");
            }

            var difference = maxValue - minValue;
            var differenceBytes = difference.ToByteArray();

            BigInteger randomNumber;
            do
            {
                byte[] randomBytes = new byte[differenceBytes.Length];
                random.NextBytes(randomBytes);
                randomNumber = new BigInteger(randomBytes);
            } while (randomNumber < 0 || randomNumber >= difference);

            return minValue + randomNumber;
        }



        public void Shuffle<T>(LinkedList<T> list)
        {
            int n = list.Count;

            List<T> tempList = new List<T>(list);

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = tempList[i];
                tempList[i] = tempList[j];
                tempList[j] = temp;
            }

            list.Clear();
            foreach (T item in tempList)
            {
                list.AddLast(item);
            }
        }



        public void callPractice()
        {

            baseLanguage.databaseList = zahlList;

            isPracticeCalled = true;
            this.navigationService.NavigateTo(nameof(PracticeView), this);

        }
    }
}



