using System.Net;
using System.Net.Sockets;

namespace BlazorApp.Services.Http;

/// <summary>
/// Class that is responsible to Communicate with the java server over a socket connection
/// </summary>
public class JavaSocketConnection:IJavaSocketConnection
{
    //private string ToSend { get; set; }
    //private IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("192.168.0.6"), 4343);
    private IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2910);
    private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private bool connected = false;
    
    /// <summary>
    /// Method that establishes a socket connection to the java server.
    /// </summary>
    public void Connect()
    {
        if (!connected)
        {
            clientSocket.Connect(serverAddress);
            connected = true;
        }
    }

    /// <summary>
    /// Method that converts a message into Json format and sends it over the socket connection
    /// to the java database
    /// </summary>
    /// <param name="date">date given to get instructors</param>
    /// <returns>Json of the lessons on the given date</returns>
    /// <exception cref="Exception">Exception if the date is null or empty</exception>
    public Task<string> SendMessage(string date)
    {
  
        if (string.IsNullOrEmpty(date))
        {
            throw new Exception($"lesson date cannot be empty!");
        }
        
        string message = "{\"Date\":\"";
        message += date;
        message += "\", \"Action\": \"get\"}";
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(message);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(message);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);
        
        byte[] rcvLenBytes = new byte[4];
        clientSocket.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        clientSocket.Receive(rcvBytes);
        String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

        Console.WriteLine("Client received: " + rcv);

        //clientSocket.Close();
        return Task.FromResult(rcv);
    }

    public void DeleteLesson(string lessonId)
    {
        if (lessonId.Equals("0"))
        {
            throw new Exception($"Incorrect lesson Id!");
        }
        
        string message = "{\"LessonId\":\"";
        message += lessonId;
        message += "\", \"Action\": \"delete\"}";
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(message);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(message);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);
    }
}