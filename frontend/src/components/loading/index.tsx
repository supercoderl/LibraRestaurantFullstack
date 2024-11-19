import Lottie from "lottie-react"

export const Loading = ({ width }: { width: any }) => {
    const options = {
        loop: true,
        autoplay: true,
        animationData: require('../../../public/assets/animation/loading.json'),
        rendererSettings: {
            preserveAspectRatio: 'xMidYMid slice',
        },
    }

    return (
        <Lottie
            animationData={options.animationData}
            autoPlay
            loop
            style={{ width }}
        />
    )
}