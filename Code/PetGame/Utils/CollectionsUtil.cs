using System.Collections.Generic;

public static class CollectionsUtil {

	public static T Random<T>(this IList<T> collection) {
		var index = Utils.Random.Next(collection.Count);
		return collection[index];
	}
	
}