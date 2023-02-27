namespace NetInterfacesCovarContrvarApp
{
    class Message
    {
        public string? Text { set; get; }
        public Message(string text)
        { 
            this.Text = text; 
        }
    }

    class EmailMessage : Message
    {
        public EmailMessage(string text) : base(text) { }
    }

    //interface IMessanger<out T>
    interface IMessanger<in T, out U>
    {
        U WriteMessage(string text);
        void SendMessage(T message);
    }

    //class EmailMessanger : IMessanger<EmailMessage>
    //{
    //    public EmailMessage WriteMessage(string text)
    //    {
    //        return new EmailMessage($"Email: {text}");
    //    }
    //}

    //class OutlookMessanger : IMessanger<Message>
    //{
    //    public void SendMessage(Message message)
    //    {
    //        Console.WriteLine($"Send message: {message.Text}");
    //    }
    //}

    class SuperMessenger : IMessanger<Message, EmailMessage>
    {
        public void SendMessage(Message message)
        {
            Console.WriteLine($"Send message: {message.Text}");
        }

        public EmailMessage WriteMessage(string text)
        {
            return new EmailMessage($"Email: {text}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ковариантность
            //IMessanger<Message> messenger1 = new EmailMessanger();
            //Message message = messenger1.WriteMessage("Hello");
            //Console.WriteLine(message.Text);

            //IMessanger<EmailMessage> client1 = new EmailMessanger();
            //IMessanger<Message> messanger2 = client1;
            //Message email = messanger2.WriteMessage("Hello world");
            //Console.WriteLine(email.Text);

            // Контрвариантность
            //IMessanger<EmailMessage> outlook = new OutlookMessanger();
            //outlook.SendMessage(new EmailMessage("Hello people"));

            //IMessanger<Message> messanger3 = new OutlookMessanger();
            //IMessanger<EmailMessage> bat = messanger3;
            //bat.SendMessage(new EmailMessage("Hello all"));

            /*
            IMessanger<Message, Message>
            IMessanger<EmailMessage, EmailMessage>
            IMessanger<Message, EmailMessage>
            IMessanger<EmailMessage, Message>
             */

            IMessanger<EmailMessage, Message> m1 = new SuperMessenger();
            Message mess1 = m1.WriteMessage("Hello 1");
            Console.WriteLine(mess1.Text);
            m1.SendMessage(new EmailMessage("Hello 2"));

            IMessanger<EmailMessage, EmailMessage> m2 = new SuperMessenger();
            EmailMessage mess2 = m2.WriteMessage("Hello 3");

            IMessanger<Message, Message> m3 = new SuperMessenger();
            Message mess3 = m3.WriteMessage("Hello 4");
            m3.SendMessage(mess3);
        }
    }
}