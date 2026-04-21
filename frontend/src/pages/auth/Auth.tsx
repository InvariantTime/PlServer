import { useState } from "react";
import { LoginPanel } from "./LoginPanel";
import { RegisterPanel } from "./RegisterPanel";

export const Auth = () => {

    const [authStep, setAuthStep] = useState<'register' | 'login'>('login');

    const goToLogin = () => {
        setAuthStep('login');
    }

    const goToRegister = () => {
        setAuthStep('register');
    }

    return (
        <div className="min-h-full w-full flex items-center justify-center">
            <div className="space-y-4 mb-8 border-2 rounded-xl border-dashed border-emerald-900 w-full max-w-md p-4">
                {authStep === 'login' && <LoginPanel goToRegister={goToRegister}/>}
                {authStep === 'register' && <RegisterPanel goToLogin={goToLogin}/>}
            </div>
        </div>
    );
}