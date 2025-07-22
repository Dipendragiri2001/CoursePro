import httpClient from "../axios-config";

function HomePage() {

  var test = httpClient.get("pokemon/ditto")
                  .then(result=>{
                    console.log(result);
                  }).catch(err=>{
                    console.log(err);
                  });
    return ( <> 
    <h1 className="text-3xl font-bold text-red-400">
    This is a home page
  </h1>
    </> );
}

export default HomePage;


