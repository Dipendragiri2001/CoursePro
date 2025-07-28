import { createBrowserRouter } from "react-router";
import Layout from "./layouts/layout";
import HomePage from "./pages/HomePage";
import ContactPage from "./pages/ContactPage";
import AdminLayout from "./layouts/admin-layout";
import AdminDashboard from "./pages/admin/Dashboard";
import CoursesPage from "./pages/admin/CoursesPage";
import StudentsPage from "./pages/admin/StudentsPage";
import { checkUserPermission, checkAdminPermission } from "./auth-check";
import httpClient from "./axios-config";
import LoginPage from "./pages/loginPage";

const AppLoader = async ({ request }: { request: Request }) => {
  checkUserPermission(request);
  try {
    const result: any = await httpClient.get("api/auth/test");

    return result.data;
  } catch (error) {
    console.error(error);
  }
};

const router = createBrowserRouter([
  {
    path: "auth/login",
    Component: LoginPage,
  },
  {
    path: "/",
    Component: Layout,
    children: [
      {
        path: "",
        Component: HomePage,
        loader: AppLoader,
      },
      {
        path: "contact",
        Component: ContactPage,
      },
    ],
  },

  {
    path: "/portal/admin",
    Component: AdminLayout,
    loader: async ({ request }: { request: Request }) => {
      checkAdminPermission(request);
    },
    children: [
      {
        path: "",
        Component: AdminDashboard,
      },
      {
        path: "dashboard",
        Component: AdminDashboard,
      },
      {
        path: "courses",
        Component: CoursesPage,
      },
      {
        path: "students",
        Component: StudentsPage,
      },
    ],
  },
]);

export default router;
