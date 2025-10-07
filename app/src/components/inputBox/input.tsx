import "./style.css";
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
