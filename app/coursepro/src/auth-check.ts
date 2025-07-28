import { redirect } from "react-router";
import type AuthResult from "./interfaces/AuthResult";


function isAuthenticated(request:Request){
    
    const authResult = localStorage.getItem('authResult');
    const url = new URL(request.url);
    if(authResult){
        const result: AuthResult = JSON.parse(authResult);

        if(new Date(result.expiresAt).getTime() <= new Date().getTime()){
            localStorage.removeItem('authResult');
            throw redirect(`/auth/login?redirectTo=${encodeURIComponent(url.pathname + url.search)}`);
        }
    }
    else{
        throw redirect(`/auth/login?redirectTo=${encodeURIComponent(url.pathname + url.search)}`);
    }
    return null;
}

export default function checkUserPermission(request:Request){
    
    isAuthenticated(request);
}

function checkAdminPermission(request:Request){
    
    isAuthenticated(request)
    debugger
    const authResult = localStorage.getItem('authResult');
    const url = new URL(request.url);
    if(authResult){
        const result: AuthResult = JSON.parse(authResult);

        if(!result.roles.includes("Admin")){
            localStorage.removeItem('authResult');
            throw redirect(`/auth/login?redirectTo=${encodeURIComponent(url.pathname + url.search)}`);
        }
    }
    else{
        throw redirect(`/auth/login?redirectTo=${encodeURIComponent(url.pathname + url.search)}`);
    }
    return null;
}

export {checkAdminPermission, checkUserPermission}
