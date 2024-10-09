import Form from "@/components/form";
import {RadioGroup, Radio} from "@nextui-org/react";
import {useEffect, useState} from "react";
import {samsonServerClient} from "@/clients/Clients";
import {Input} from "@nextui-org/input";

export default function ListenSettings() {
    const [listeningMode, setListeningMode] = useState<string | undefined>("");
	const [listeningDuration, setListenDuration] = useState<string | undefined>("");
    const [activationHotkey, setActivationHotkey] = useState<string>();

    useEffect(() => {
        const GetListenSettings = async(id: number) => {
            const userSettings = await samsonServerClient.settings(8);
            setListenDuration(userSettings.listenDuration?.toString());
            setListeningMode(userSettings.listenMode?.toString());
        }
        
        GetListenSettings(8);
    }, []);
    
    return (
        <Form buttonText="Save">
            <label>Listening Settings</label>

            <div className="flex flex-row justify-between items-center w-full">
                <RadioGroup
                    label="Select your preferred listening mode"
                    value={listeningMode}
                    onValueChange={setListeningMode}
                >
                    <Radio value="1">wake word only</Radio>
                    <Radio value="2">always on - (Does not exist yet but its here)</Radio>
                    <Radio value="3">manual activation - (Also does not exist yet but its here)</Radio>
                </RadioGroup>
            </div>

            {listeningMode === "1" ?
                <div className="flex flex-row justify-between items-center w-full pt-10">
                    <RadioGroup
                        label="Select your preferred listen duration (duration of audio that is captured between evaluations for wake words - can affect performance and speed of response)"
                        value={listeningDuration}
                        onValueChange={setListenDuration}
                    >
                        <Radio value="5">5 seconds</Radio>
                        <Radio value="10">10 seconds</Radio>
                        <Radio value="15">15 seconds</Radio>
                    </RadioGroup>
                </div> : <></>}

            {listeningMode === "3" ? <div className="flex flex-row justify-between items-center w-full">
                <Input type="text" label="Enter Activation hotkey" value={activationHotkey} onChange={(e) => setActivationHotkey(e.target.value)}></Input>
            </div> : <></>}
        </Form>
    )
}