import Form from "@/components/form";
import { Input } from "@nextui-org/input";

export default function ServerSettings() {
    return (
        <Form>
            <label>Connect to your Custom Samson Server Instance!</label>
            <Input type="text" label="Samson Server Url" placeholder="Enter samson server url"></Input>
            <Input type="email" label="Email" placeholder="Enter email"></Input>
            <Input type="password" label="Password" placeholder="Enter password"></Input>
        </Form>
    )
}