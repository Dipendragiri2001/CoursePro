import { redirect } from "react-router";

export default function handleHttpError(status: number){
    switch (status) {
        case 401:
            throw redirect(`/auth/login?redirectTo=${encodeURIComponent(window.location.pathname + window.location.search)}`)
    
        default:
            break;
    }
}