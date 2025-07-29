import { createBrowserRouter, redirect } from "react-router";
import Layout from "./layouts/layout";
import HomePage from "./pages/HomePage";
import ContactPage from "./pages/ContactPage";
import AdminLayout from "./layouts/admin-layout";
import AdminDashboard from "./pages/admin/Dashboard";
import CoursesPage from "./pages/admin/CoursesPage";
import StudentsPage from "./pages/admin/StudentsPage";
import { checkUserPermission, checkAdminPermission } from "./auth-check";
import httpClient from "./axios-config";
import LoginPage from "./pages/LoginPage";
import handleHttpError from "./handle-http-error";

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
        loader: async ({ request }: { request: Request }) => {
          try {
            const result: any = await httpClient.get("api/auth/test");
            return result.data;
          } catch (error: any) {
            handleHttpError(error.status);
          }
        },
      },
    ],
  },

  {
    path: "/portal/admin",
    Component: AdminLayout,
    children: [
      {
        path: "",
        Component: AdminDashboard,
        loader: async ({ request }: { request: Request }) => {
          checkAdminPermission(request);
        },
      },
      {
        path: "dashboard",
        Component: AdminDashboard,
        loader: async ({ request }: { request: Request }) => {
          checkAdminPermission(request);
        },
      },
      {
        path: "courses",
        Component: CoursesPage,
        loader: async ({ request }: { request: Request }) => {
          checkAdminPermission(request);
        },
      },
      {
        path: "students",
        Component: StudentsPage,
        loader: async ({ request }: { request: Request }) => {
          checkAdminPermission(request);
        },
      },
    ],
  },
]);

export default router;
