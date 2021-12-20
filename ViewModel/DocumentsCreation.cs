using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Model;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Windows;

namespace PetShelter.ViewModel
{
    class DocumentsCreation
    {
        public string GenerateContract(Contract contract, Animal animal, Client client, InfoDepEmploee emploee, Emploee empData)
        {
            Word._Application word = new Word.ApplicationClass();

            object missing = Type.Missing;
            Word._Document document = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            for (int i = 0; i < 40; i++)
                document.Paragraphs.Add();

            Word.Range rng = document.Paragraphs[1].Range;
            rng.Text = "Договір № " + contract.ContractNum.ToString();
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[2].Range;
            rng.Text = "про передачу тварини на постійне утримання";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[4].Range;
            rng.Text = "м. Харків                                                                               " + contract.SigningDate.Value.Date.ToString().Substring(0, 11);

            rng = document.Paragraphs[6].Range;
            rng.Text = "Харківська міська громадська організація «Притулок для тварин», " +
                "іменована надалі «Колишній власник» та " + client.SecondName + " " + client.FirstName + " " + client.ThirdName +
                " паспорт номер " + client.IDCardNum + " виданий " + client.IDCardSeries + ". Фактична адреса проживання " +
                client.Region + " Область, місто " + client.City + " вулиця " + client.Street + " будинок " + client.BuildingNum + " квартира " + client.FlatNum +
                "іменований надалі «Новий власник», разом іменовані «Сторони», уклали цей договір про таке:";

            rng = document.Paragraphs[8].Range;
            rng.Text = "Предмет договору";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[9].Range;
            rng.Text = "Колишній власник передає новому власнику, а Новий власник приймає для утримання як домашню тварину: ";

            rng = document.Paragraphs[10].Range;
            rng.Text = "Вид/ім'я/стать/вік: " + animal.AnimalKind + ", " + animal.Name + ", " + animal.Sex + ", " + Math.Floor((DateTime.Now - animal.BirthDate).Value.Days / 365d);

            rng = document.Paragraphs[15].Range;
            rng.Text = "Контактні дані та адреси сторін:";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[16].Range;
            rng.Text = "        Колишній власник                                                            Новий власник";

            rng = document.Paragraphs[17].Range;
            rng.Text = "Відповідальний за укладання договору:";

            rng = document.Paragraphs[18].Range;
            rng.Text = $"{empData.SecondName} {empData.FirstName} {empData.ThirdName}                     {client.SecondName} {client.FirstName} {client.ThirdName}";

            rng = document.Paragraphs[19].Range;
            rng.Text = $"Телефон: {emploee.Phone}                               Телефон: +38{client.Phone}";

            rng = document.Paragraphs[20].Range;
            rng.Text = $"E-mail: {emploee.Email}                    E-mail: {client.Email}";

            rng = document.Paragraphs[21].Range;
            rng.Text = $"Підпис:                                                                    Підпис: ";


            document.Activate();
            word.Visible = true;

            object save_changes = false;
            object filename = null;

            
            

            return filename == null ? "" : filename.ToString();
        }

        public void GenerateInfoCard(Animal animal)
        {
            Word._Application word = new Word.ApplicationClass();

            object missing = Type.Missing;
            Word._Document document = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            for (int i = 0; i < 40; i++)
                document.Paragraphs.Add();

            Word.Range rng = document.Paragraphs[1].Range;
            rng.Text = $"Інформаційна картка тварини: {animal.Name}";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[3].Range;
            rng.Text = "Дата: " + DateTime.Now.Date.ToString().Substring(0, 11);

            rng = document.Paragraphs[4].Range;
            rng.Text = "Вид тварини: " + animal.AnimalKind;
            rng = document.Paragraphs[5].Range;
            rng.Text = "Стать: " + animal.Sex;
            rng = document.Paragraphs[6].Range;
            rng.Text = "Окрас: " + animal.Color;
            rng = document.Paragraphs[7].Range;
            rng.Text = "Вага: " + animal.Weight;
            rng = document.Paragraphs[8].Range;
            rng.Text = "Висота: " + animal.Height;
            rng = document.Paragraphs[9].Range;
            rng.Text = "Дата народження: " + animal.BirthDate.Value.Date.ToString().Substring(0, 11);
            rng = document.Paragraphs[10].Range;
            rng.Text = "Дата Регістрації: " + animal.RegistrationDate.Value.Date.ToString().Substring(0, 11);
            rng = document.Paragraphs[11].Range;
            rng.Text = "Днів у карантині: " + animal.QuarantineDays;
            rng = document.Paragraphs[12].Range;
            rng.Text = "Кімната: " + animal.Room.Name;

            rng = document.Paragraphs[13].Range;
            rng.Text = "Інформація про групу: " + animal.GroupID;
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[15].Range;
            rng.Text = "Описання: " + animal.Group.Description;
            rng = document.Paragraphs[16].Range;
            rng.Text = "Додатковий догляд: " + animal.Group.AdditionalCare;

            rng = document.Paragraphs[17].Range;
            rng.Text = "Інформація про стани: ";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            int count = 18;

            foreach (StateValue sv in animal.StateValues)
            {
                rng = document.Paragraphs[++count].Range;
                rng.Text = sv.State.Name;
                rng = document.Paragraphs[++count].Range;
                rng.Text = "Значення: " + sv.Value;
            }

            document.Activate();
            word.Visible = true;
        }

    }
}
