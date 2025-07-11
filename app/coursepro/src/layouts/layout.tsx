import { NavLink, Outlet } from "react-router";

function Layout() {
    return ( 
    <>
    <div>
        <ul>
            <li>
                <NavLink to="/">Home</NavLink>
            </li>
            <li>
                <NavLink to="/contact">Contact</NavLink>
            </li>
        </ul>
    </div>
    
    <div>
        <Outlet></Outlet>
    </div>

    </> );
}

export default Layout;      