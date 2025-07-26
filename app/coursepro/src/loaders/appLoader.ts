import isUserAuthenticated from "../auth-check";
import httpClient from "../axios-config";

const LoadAppData = async ({ request }: { request: Request }) => {
    isUserAuthenticated(request);
    try {
        const result: any = await httpClient.get("pokemon/ditto");
        console.log(result.data)
        return result.data;
    } catch (error) {
        console.error(error);
    }
    
  };


  export default LoadAppData;