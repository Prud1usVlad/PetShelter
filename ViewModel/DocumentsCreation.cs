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

            for (int i = 0; i < 140; i++)
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
            rng.Text = "1. Предмет договору";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[9].Range;
            rng.Text = "Колишній власник передає новому власнику, а Новий власник приймає для утримання як домашню тварину: ";

            rng = document.Paragraphs[10].Range;
            rng.Text = "Вид/ім'я/стать/вік: " + animal.AnimalKind + ", " + animal.Name + ", " + animal.Sex + ", " + Math.Floor((DateTime.Now - animal.BirthDate).Value.Days / 365d);

            rng = document.Paragraphs[12].Range;
            rng.Text = "2. Права сторін.";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[14].Range;
            rng.Text = "2.1. При здійсненні прав не допускається жорстоке поводження з тваринами, що суперечить принципам гуманності " +
                ". Новий господар із цим пунктом ознайомлений ________________________ (підпис).";

            rng = document.Paragraphs[16].Range;
            rng.Text = "2.2. Права Нового власника:";
            rng.Bold = 1;

            rng = document.Paragraphs[18].Range;
            rng.Text = "Отримати від колишнього власника тварину, документи на тварину," +
                " а також повну та достовірну інформацію про стан здоров'я та особливості поведінки тварини.";

            rng = document.Paragraphs[21].Range;
            rng.Text = "2.3. Права Колишнього власника:";
            rng.Bold = 1;

            rng = document.Paragraphs[23].Range;
            rng.Text = "2.3.1. Здійснювати патронаж над твариною після передачі тварини Новому власнику у будь-який час;";

            rng = document.Paragraphs[24].Range;
            rng.Text = "2.3.2. Пред'явити до суду вимогу про вилучення тварини у разі жорстокого поводження з твариною, " +
                "а також неналежного утримання тварини. Колишній власник має право вимагати у досудовому порядку, " +
                "а також у судовому порядку у разі виникнення спору, всі витрати пов'язані з вилученням тварини, " +
                "перебуванням її під опікою третіх осіб (перетримки), лікуванням тварини, судових витрат.";

            rng = document.Paragraphs[26].Range;
            rng.Text = "3. Обов'язки сторін.";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[27].Range;
            rng.Text = "3.1. Обов'язки Колишнього власника:";
            rng.Bold = 1;

            rng = document.Paragraphs[29].Range;
            rng.Text = "3.1.1.Консультувати Нового власника з питань утримання та виховання тварини;";

            rng = document.Paragraphs[30].Range;
            rng.Text = "3.1.2. Надавати допомогу в кастрації/стерилізації тварини після досягнення статевого дозрівання " +
                "(у разі, якщо така операція не була здійснена раніше).";

            rng = document.Paragraphs[31].Range;
            rng.Text = "3.1.3. У разі неможливості утримання тварини Новим власником, надати допомогу в пошуку" +
                " іншого господаря за умови проживання тварини у Нового власника до моменту прибудови в новий будинок.";

            rng = document.Paragraphs[33].Range;
            rng.Text = "3.2.Обов'язки Нового власника:";
            rng.Bold = 1;

            rng = document.Paragraphs[35].Range;
            rng.Text = "3.2.1.Забезпечувати відповідний утримання та годівлю тварини відповідно до зоогігієнічних вимог.";

            rng = document.Paragraphs[36].Range;
            rng.Text = "3.2.2.При необхідності надавати тварині ветеринарну допомогу;";

            rng = document.Paragraphs[37].Range;
            rng.Text = "3.2.3.Не перешкоджати патронажу над твариною з боку колишнього власника;";

            rng = document.Paragraphs[38].Range;
            rng.Text = "3.2.4. Негайно повідомити Колишнього власника про зміну місця проживання " +
                "та/або контактного телефону протягом 3-х днів;";

            rng = document.Paragraphs[39].Range;
            rng.Text = "3.2.5. У разі втрати або смерті тварини не з вини та недбалості " +
                "Нового власника повідомити про це колишнього власника протягом 3-х діб " +
                "з моменту настання події (втрати або смерті);";

            rng = document.Paragraphs[40].Range;
            rng.Text = "3.2.6. Новий власник зобов'язується відповідно до цього договору " +
                "нести повну матеріальну відповідальність за передану тварину;";

            rng = document.Paragraphs[41].Range;
            rng.Text = "3.2.7. Не присипляти тварину без повідомлення та погодження з колишнім власником;";

            rng = document.Paragraphs[42].Range;
            rng.Text = "3.2.8. Не продавати і не передавати тварину третім особам," +
                " за винятком тимчасової передачі у зв'язку з відрядженням, перебуванням у лікарні," +
                " на лікування та відпустці.";

            rng = document.Paragraphs[43].Range;
            rng.Text = "3.2.9. Безоплатно та безперешкодно повернути тварину Колишньому власнику у" +
                " разі виявлення недотримання умов цього договору та/або жорстокого поводження з твариною;";

            rng = document.Paragraphs[44].Range;
            rng.Text = "3.2.10. Стерилізувати/каструвати тварину після досягнення статевого дозрівання " +
                "(у разі, якщо така операція не була здійснена раніше).";

            rng = document.Paragraphs[46].Range;
            rng.Text = "4.Відповідальність сторін.";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[47].Range;
            rng.Text = " Сторона, яка не виконала будь-яке зобов'язання за Договором, або що виконала його неналежним чином," +
                " несе відповідальність перед іншою стороною відповідно до законодавства України.";

            rng = document.Paragraphs[49].Range;
            rng.Text = "5. Порядок дії договору. Інші умови.";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[51].Range;
            rng.Text = "5.1. Цей договір вважається таким, що набрав законної сили, " +
                "а права та обов'язки настали, з моменту підписання цього договору та передачі тварини Новому власнику.";

            rng = document.Paragraphs[52].Range;
            rng.Text = "5.2. Новий власник відповідає за тварину відповідно до законодавства України.";

            rng = document.Paragraphs[53].Range;
            rng.Text = "5.3. Цей договір укладено на невизначений термін.";

            rng = document.Paragraphs[55].Range;
            rng.Text = "6.Дія договору припиняється:";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[57].Range;
            rng.Text = "6.1. У разі смерті тварини не з вини та недбалості Нового власника.";

            rng = document.Paragraphs[58].Range;
            rng.Text = "6.2.У разі смерті Нового власника та відсутності у нього правонаступників (наприклад, родичів).";

            rng = document.Paragraphs[59].Range;
            rng.Text = "6.3. Договір складено на 4 сторінках українською у двох примірниках, " +
                "по одному для кожної зі сторін, що мають однакову юридичну силу.";

            rng = document.Paragraphs[70].Range;
            rng.Text = "Контактні дані та адреси сторін:";
            rng.Bold = 1;
            rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            rng = document.Paragraphs[71].Range;
            rng.Text = "        Колишній власник                                                            Новий власник";

            rng = document.Paragraphs[72].Range;
            rng.Text = "Відповідальний за укладання договору:";

            rng = document.Paragraphs[73].Range;
            rng.Text = $"{empData.SecondName} {empData.FirstName} {empData.ThirdName}                     {client.SecondName} {client.FirstName} {client.ThirdName}";

            rng = document.Paragraphs[74].Range;
            rng.Text = $"Телефон: {emploee.Phone}                               Телефон: +38{client.Phone}";

            rng = document.Paragraphs[75].Range;
            rng.Text = $"E-mail: {emploee.Email}                    E-mail: {client.Email}";

            rng = document.Paragraphs[76].Range;
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
