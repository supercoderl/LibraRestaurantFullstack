import { Stack } from 'expo-router'
import { RefreshControl, ScrollView } from 'react-native'

import {
  BannerTwo,
  BestSellsSlider,
  Categories,
  DiscountSlider,
  Slider as MainSlider,
  MostFavouraiteProducts,
  FeedHeader,
  ShowWrapper,
} from '@/components'
import { banners } from '@/models/banner'
import { events } from 'models/event'
import { useGetFeedInfoQuery } from 'services'

export default function FeedScreen() {
  //? Assets

  //? Get Feeds Query
  const { categories, menuItems, isFetching, refetch } = useGetFeedInfoQuery(
    {},
    {
      selectFromResult: ({ data, ...args }) => ({
        categories: data?.data?.categories?.items || [],
        menuItems: data?.data?.menuItems?.items || [],
        ...args,
      }),
    }
  )

  //? Render(s)
  return (
    <>
      <Stack.Screen
        options={{
          header: props => <FeedHeader {...props} title="Home" icon="menu-outline" />,
        }}
      />
      <ShowWrapper
        error={null}
        isError={false}
        refetch={() => {}}
        isFetching={false}
        isSuccess={true}
        type="detail"
      >
        <ScrollView
          className="bg-white flex h-full px-3"
          refreshControl={<RefreshControl refreshing={isFetching} onRefresh={refetch} />}
        >
          <>
            <MainSlider data={banners} />
            <Categories categories={categories} homePage />
            <DiscountSlider products={menuItems.slice(0, 5)} isLoading={isFetching} />
            <BestSellsSlider products={menuItems.slice(5, 10)} isLoading={isFetching} />
            <BannerTwo data={events} />
            <MostFavouraiteProducts products={menuItems.slice(10)} isLoading={isFetching} />
          </>
        </ScrollView>
      </ShowWrapper>
    </>
  )
}
