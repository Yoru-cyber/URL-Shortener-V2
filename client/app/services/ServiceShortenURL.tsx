import IResponse from "~/interfaces/IResponse";
export const shortenUrl = async (url: string): Promise<IResponse> => {
  try {
    const response = await fetch("http://localhost:3001/shorten", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
      },
      body: JSON.stringify({ url: url }),
    });

    if (!response.ok) {
      throw new Error("Network response was not ok");
    }

    const data: IResponse = await response.json();
    return data;
  } catch (error) {
    throw new Error(`Failed to shorten URL: ${(error as Error).message}`);
  }
};
