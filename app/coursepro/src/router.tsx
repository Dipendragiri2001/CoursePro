import { createBrowserRouter } from "react-router";
import Layout from "./layouts/layout";
import HomePage from "./pages/HomePage";
import ContactPage from "./pages/ContactPage";

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
  ]);


  export default router;