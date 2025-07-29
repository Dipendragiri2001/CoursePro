import { useLoaderData } from "react-router";
import httpClient from "../axios-config";
import { Button } from "antd";

function HomePage() {
  const appData = useLoaderData();

  // var test = httpClient.get("pokemon/ditto")
  //                 .then(result=>{
  //                   console.log(result);
  //                 }).catch(err=>{
  //                   console.log(err);
  //                 });
  return (
    <>
      <h1 className="text-3xl font-bold text-red-400">This is a home page</h1>
    </>
  );
}

export default HomePage;
