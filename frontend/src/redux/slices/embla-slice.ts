import { createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';
import { EmblaCarouselType } from 'embla-carousel';

//stateType
type useEmblaType={
  emblaApi: EmblaCarouselType | null;
}


//initialState
const initialState : useEmblaType = {
  emblaApi: null
}


//slice
export const emblaSlice = createSlice({
  name: 'embla',
  initialState,
  reducers: {
    setEmbla:(state: Draft<typeof initialState>, action: PayloadAction<EmblaCarouselType>)=>{
      state.emblaApi = action.payload;
    },
  },
});

// Reducers and actions
export const { setEmbla } = emblaSlice.actions;

export default emblaSlice.reducer;