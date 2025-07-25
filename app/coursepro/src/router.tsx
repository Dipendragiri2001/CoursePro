import { createBrowserRouter } from "react-router";
import Layout from "./layouts/layout";
import HomePage from "./pages/HomePage";
import ContactPage from "./pages/ContactPage";
import AdminLayout from "./layouts/admin-layout";
import AdminDashboard from "./pages/admin/Dashboard";
import CoursesPage from "./pages/admin/CoursesPage";
import StudentsPage from "./pages/admin/StudentsPage";

const router = createBrowserRouter([
    {
      path: "/",
      Component: Layout,
      children:[
        {
            path:'',
            Component: HomePage
        },
        {
            path: 'contact',
            Component: ContactPage
        }
      ]
    },

    {
      path: "/portal/admin",
      Component: AdminLayout,
      children:[
        {
            path:'',
            Component: AdminDashboard
        },
        {
            path: 'dashboard',
            Component: AdminDashboard
        },
        {
          path: 'courses',
          Component: CoursesPage
        },
        {
          path: 'students',
          Component: StudentsPage
        }
      ]
    },

  ]);


  export default router;