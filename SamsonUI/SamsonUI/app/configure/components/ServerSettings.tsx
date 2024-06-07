import Form from "@/components/form";
import { SamsonTokenContext } from "@/config/globals";
import { Input } from "@nextui-org/input";
import { useContext, useState } from "react";

export type SamsonCredentials = {

}

export default function ServerSettings() {

    const [serverUrl, setServerUrl] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const tokenContext = useContext(SamsonTokenContext);

    const onSave = async () => {
        const formData = new FormData();
        formData.append("email", email);
        formData.append("password", password);
        formData.append("username", "");
        const response = await fetch(`${serverUrl}/Users/login`, {
            body: formData,
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            }
        });
        
        const token = await response.json() as string;
        console.log(token);
        // tokenContext?.setToken(token);
        // console.log(tokenContext?.token);
    }

    return (
        <Form buttonText="Save" onClick={onSave}>
            <label>Connect to your Custom Samson Server Instance!</label>
            <Input value={serverUrl} onValueChange={setServerUrl} type="text" label="Samson Server Url" placeholder="Enter samson server url"></Input>
            <Input value={email} onValueChange={setEmail} type="email" label="Email" placeholder="Enter email"></Input>
            <Input value={password} onValueChange={setPassword} type="password" label="Password" placeholder="Enter password"></Input>
        </Form>
    )
}