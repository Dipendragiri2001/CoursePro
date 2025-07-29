import { NavLink, Outlet, useNavigate } from "react-router";

function Layout() {
  const navigate = useNavigate();
  function logout() {
    localStorage.removeItem("authResult");
    navigate("/auth/login");
  }

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
          <li>
            <NavLink to="/portal/admin">Admin Panel</NavLink>
          </li>
        </ul>
      </div>
      <div>
        <button onClick={logout}>Log out</button>
      </div>

      <div>
        <Outlet></Outlet>
      </div>
    </>
  );
}

export default Layout;
