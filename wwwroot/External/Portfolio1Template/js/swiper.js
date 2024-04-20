jQuery(window).on( 'pluginSwiperReady', function(){
    var swiperParent = new Swiper('.swiper-parent',{
        paginationClickable: false,
        slidesPerView: 4,
        grabCursor: true,
        navigation: {
            nextEl: '.slider-arrow-right',
            prevEl: '.slider-arrow-left',
        },
        breakpoints: {
            1200: {
              slidesPerView: 4
            },
            992: {
              slidesPerView: 3
            },
            768: {
              slidesPerView: 2
            }
         }
    });

    var swiperNested1 = new Swiper('.swiper-nested-1',{
        direction: 'vertical',
        paginationClickable: false,
        slidesPerView: 3,
        navigation: {
            nextEl: '#sw1-arrow-bottom',
            prevEl: '#sw1-arrow-top',
        },
        breakpoints: {
            1200: {
              slidesPerView: 3
            },
            992: {
              slidesPerView: 2
            }
         }
    });

    var swiperNested2 = new Swiper('.swiper-nested-2',{
        direction: 'vertical',
        paginationClickable: false,
        slidesPerView: 2,
        navigation: {
            nextEl: '#sw2-arrow-bottom',
            prevEl: '#sw2-arrow-top',
        },
        breakpoints: {
            1200: {
              slidesPerView: 2
            }
         }
    });

    var swiperNested3 = new Swiper('.swiper-nested-3',{
        direction: 'vertical',
        paginationClickable: false,
        slidesPerView: 2,
        navigation: {
            nextEl: '#sw3-arrow-bottom',
            prevEl: '#sw3-arrow-top',
        },
        breakpoints: {
            1200: {
              slidesPerView: 2
            }
         }
    });
});