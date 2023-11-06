from transformers import AutoModelForCausalLM, AutoTokenizer

# Use a pipeline as a high-level helper
from transformers import pipeline
pipe = pipeline("text-generation", model="nomic-ai/gpt4all-j")


async def queryForAll(input: str):
    output = pipe("hi do you work")
    print(output)