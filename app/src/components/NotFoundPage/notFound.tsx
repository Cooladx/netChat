import { Link } from "react-router";

export default function NotFoundPage ({setLoggedIn}: {  setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;}) {
         setLoggedIn(false);
    return (
        <div>
            <h1>Not Found Page</h1>
            
            <Link to={"/login"}>
            <button>Go back Home</button>
            </Link>
        </div>
    )
} 