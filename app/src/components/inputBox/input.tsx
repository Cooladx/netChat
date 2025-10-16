import "./style.css";

/* 
Component to build an input box for the client to type into. 
The state value will be tracked on user typing into box and set to that current state.
These input values will mainly be used for sending to the server to be processed.  
*/
export default function Input({
  placeholder,
  value,
  onValueChange,
}: {
  placeholder: string;
  value: string;
  onValueChange: React.Dispatch<React.SetStateAction<string>>;
}) {
  return (
    <>
      <input
        type="text"
        className="messageInput"
        placeholder={placeholder}
        onChange={(e) => {
          onValueChange(e.target.value);
        }}
        value={value}
      />
    </>
  );
}
