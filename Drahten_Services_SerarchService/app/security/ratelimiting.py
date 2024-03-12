from ratelimit import limits

# Time period for request limiting in seconds.
TIME_PERIOD = 60 

@limits(calls=100, period=TIME_PERIOD)
# Decorator function.
# The purpose of this function is to serve as point of registration for the @limits decorator.
def rate_limiter():
    return