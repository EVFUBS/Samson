import Form from "@/components/form";
import {RadioGroup, Radio} from "@nextui-org/react";
import { useState } from "react";

export default function ListenSettings() {
    const [selectedListeningMode, setSelectedListeningMode] = useState("wake");
	const [selectedListeningDuration, setSelectedListenDuration] = useState("");

    return (
        <Form buttonText="Save">
            <label>Listening Settings</label>
            <div className="flex flex-row justify-between items-center w-full">
                <RadioGroup 
                    label="Select your preferred listening mode"
                    value={selectedListeningMode}
                    onValueChange={setSelectedListeningMode}
                    >
                    <Radio value="wake">wake word only</Radio>
                    <Radio value="always">always on - (Does not exist yet but its here)</Radio>
                    <Radio value="manual">manual activation - (through UI or hotkey)</Radio>
                </RadioGroup>
            </div>

            {selectedListeningMode === "wake" ?
            <div className="flex flex-row justify-between items-center w-full pt-10">
                <RadioGroup 
                label="Select your preferred listen duration (duration of audio that is captured between evaluations for wake words - can affect performance and speed of response)"
                value={selectedListeningDuration}
                onValueChange={setSelectedListenDuration}
                >
                    <Radio value="5">5 seconds</Radio>
                    <Radio value="10">10 seconds</Radio>
                    <Radio value="15">15 seconds</Radio>
                </RadioGroup>
            </div> : <></>}
        </Form>
    )
}