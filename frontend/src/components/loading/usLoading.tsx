import Lottie from "lottie-react"
import { UsContainer } from "./style"

export const UsLoading = () => {
    const options = {
        loop: true,
        autoplay: true,
        animationData: require('../../../public/assets/animation/us-loading.json'),
        rendererSettings: {
            preserveAspectRatio: 'xMidYMid slice',
        },
    }

    return (
        <UsContainer>
            <Lottie
                animationData={options.animationData}
                autoPlay
                loop
                style={{ width: "100%", height: "100%" }}
            />
        </UsContainer>
    )
}