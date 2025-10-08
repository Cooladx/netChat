import "./style.css";

/*
Function to create a messageBox for clients to view their messages.
Logic that comes from send message button will send the information the client has put in the input boxes
and create a li element for each of those messages. The li elements (bullets) will contain the user and the message.
*/
export default function MessageBox() {
  return (
    <>
      <div className="msg-box" id="messageBox">
        {" "}
        <h2 className="messageTitle">MESSAGES RECIEVED:</h2>
        <ul className="msg-list" id="msgList"></ul>
      </div>
    </>
  );
}
