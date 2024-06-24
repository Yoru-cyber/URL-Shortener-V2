import { Link } from "@remix-run/react";

export default function NavBar(){
    return (
      <>
        <div className="text-white p-2">
          <Link to="/" className="mr-2">Home</Link>
          <Link to="/about">About</Link>
        </div>
      </>
    );
}
